(function () {
    'use strict';

    console.log('Authentication Interceptor Loading...');

    function generateUUID() {
        return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
            const r = Math.random() * 16 | 0;
            const v = c === 'x' ? r : (r & 0x3 | 0x8);
            return v.toString(16);
        });
    }

    async function calculateSha256Signature(data, requestId, secret) {
        try {
            const message = (data || '') + requestId + secret;
            const msgBuffer = new TextEncoder().encode(message);
            const hashBuffer = await crypto.subtle.digest('SHA-256', msgBuffer);
            const hashArray = Array.from(new Uint8Array(hashBuffer));
            const hashBinary = String.fromCharCode.apply(null, hashArray);
            return btoa(hashBinary);
        } catch (error) {
            console.error('Error calculating signature:', error);
            return '';
        }
    }

    function getRequestBody(req) {
        if (!req.body) return '';
        if (typeof req.body === 'string') return req.body;
        if (req.body instanceof FormData) return '';
        return JSON.stringify(req.body);
    }

    const authHandlers = {
        /**
         * MerchantAuth Handler
         * Header: X-Merchant-Auth
         * Format: merchant-number:hash
         * Generates: merchant-number, request-id, signature headers
         */
        'X-Merchant-Auth': async function (req, headerValue) {
            const parts = headerValue.split(':');
            if (parts.length !== 2) {
                console.error('[Fail] Invalid MerchantAuth format. Expected: merchant-number:hash');
                return false;
            }

            const merchantNumber = parts[0].trim();
            const merchantHash = parts[1].trim();

            if (!merchantNumber || !merchantHash) {
                console.error('[Fail] MerchantAuth: Missing merchant-number or hash');
                return false;
            }

            const requestId = generateUUID();
            const requestBody = getRequestBody(req);
            const signature = await calculateSha256Signature(requestBody, requestId, merchantHash);

            delete req.headers['X-Merchant-Auth'];

            req.headers['merchant-number'] = merchantNumber;
            req.headers['request-id'] = requestId;
            req.headers['signature'] = signature;

            console.log('[Success] MerchantAuth headers added', {
                'merchant-number': merchantNumber,
                'request-id': requestId,
                'signature': signature.substring(0, 20) + '...'
            });

            return true;
        },
        /**
         * IntegrationAuth Handler
         * Header: X-Integration-Auth
         * Format: signature-salt (user-provided SignatureSalt)
         * Generates: signature header using provided SignatureSalt
         */
        'X-Integration-Auth': async function (req, headerValue) {
            const signatureSalt = headerValue.trim();
            
            if (!signatureSalt) {
                console.error('[Fail] IntegrationAuth: Missing SignatureSalt');
                return false;
            }

            const requestBody = getRequestBody(req);
            const signature = await calculateSha256Signature(requestBody, '', signatureSalt);

            delete req.headers['X-Integration-Auth'];
            req.headers['signature'] = signature;

            console.log('[Success] IntegrationAuth headers added', {
                'signature': signature.substring(0, 20) + '...'
            });

            return true;
        }
    };

    function initializeAuthInterceptor() {
        const originalFetch = window.ui.fn.fetch;

        window.ui.fn.fetch = async function (req) {
            req.headers = req.headers || {};

            for (const [headerName, handler] of Object.entries(authHandlers)) {
                const headerValue = req.headers[headerName];

                if (headerValue) {
                    try {
                        await handler(req, headerValue);
                    } catch (error) {
                        console.error(`Error in ${headerName} handler:`, error);
                    }
                }
            }

            return originalFetch.call(this, req);
        };

        console.log('[Success] Ezzygate authentication interceptor initialized');
        console.log('Registered auth handlers:', Object.keys(authHandlers).join(', '));
    }

    function waitForSwaggerUI() {
        let attempts = 0;
        const maxAttempts = 50;
        const interval = 100;

        const checkSwaggerUI = () => {
            attempts++;

            if (window.ui && window.ui.fn && window.ui.fn.fetch) {
                initializeAuthInterceptor();
            } else if (attempts < maxAttempts) {
                setTimeout(checkSwaggerUI, interval);
            } else {
                console.error('[Fail] Failed to initialize auth interceptor: Swagger UI not found');
            }
        };

        checkSwaggerUI();
    }

    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', waitForSwaggerUI);
    } else {
        waitForSwaggerUI();
    }
})();