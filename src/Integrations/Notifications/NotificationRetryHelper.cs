namespace XIVSubmarinesRewrite.Integrations.Notifications;

using System;
using System.Globalization;
using System.Linq;
using System.Net.Http;

internal static class NotificationRetryHelper
{
    public static TimeSpan ParseRetryAfter(HttpResponseMessage response)
    {
        if (response.Headers.TryGetValues("Retry-After", out var values))
        {
            var first = values.FirstOrDefault();
            if (double.TryParse(first, NumberStyles.Float, CultureInfo.InvariantCulture, out var seconds))
            {
                return TimeSpan.FromSeconds(Math.Max(1, seconds));
            }
        }

        if (response.Headers.TryGetValues("X-RateLimit-Reset-After", out var resetValues))
        {
            var first = resetValues.FirstOrDefault();
            if (double.TryParse(first, NumberStyles.Float, CultureInfo.InvariantCulture, out var seconds))
            {
                return TimeSpan.FromSeconds(Math.Max(1, seconds));
            }
        }

        return TimeSpan.FromSeconds(30);
    }
}
