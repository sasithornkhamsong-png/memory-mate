using UnityEngine;
using System;

#if UNITY_ANDROID
using Unity.Notifications.Android;
#endif

public class NotificationManager : MonoBehaviour
{
    void Start()
    {
        ScheduleNotification();
    }

    void ScheduleNotification()
    {
#if UNITY_ANDROID

        // สร้าง Channel
        var channel = new AndroidNotificationChannel()
        {
            Id = "reminder_channel",
            Name = "Reminder Notification",
            Importance = Importance.Default,
            Description = "Game Reminder"
        };

        AndroidNotificationCenter.RegisterNotificationChannel(channel);

        // ตั้งแจ้งเตือน
        var notification = new AndroidNotification();

        notification.Title = "คิดถึงกันบ้างมั้ย 👀";
        notification.Text = "กลับมาฝึกสมองกันต่อได้น้า";
        notification.FireTime = DateTime.Now.AddDays(2);

        AndroidNotificationCenter.SendNotification(
            notification,
            "reminder_channel"
        );

#endif
    }
}