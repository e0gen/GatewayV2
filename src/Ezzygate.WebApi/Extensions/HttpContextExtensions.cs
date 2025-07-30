using Ezzygate.Domain.Models;

namespace Ezzygate.WebApi.Extensions;

public static class HttpContextExtensions
{
    private const string MerchantKey = "merchant";
    private const string CurrentDeviceKey = "currentDevice";

    public static void SetMerchant(this HttpContext context, Merchant? merchant)
    {
        context.Items[MerchantKey] = merchant;
    }

    public static Merchant? GetMerchant(this HttpContext context)
    {
        context.Items.TryGetValue(MerchantKey, out var merchant);
        return merchant as Merchant;
    }

    public static void SetCurrentDevice(this HttpContext context, MobileDevice? device)
    {
        context.Items[CurrentDeviceKey] = device;
    }

    public static MobileDevice? GetCurrentDevice(this HttpContext context)
    {
        context.Items.TryGetValue(CurrentDeviceKey, out var device);
        return device as MobileDevice;
    }
}
