using System;
using UnityEngine;
using Unity.Notifications.Android;

public class ReminderManager : MonoBehaviour
{
    void Start()
    {
        CreateChannel();
        AndroidNotificationCenter.CancelAllNotifications(); // เปิดแอป → ล้างของเก่า
    }

    void OnApplicationPause(bool paused)
    {
        if (paused)
        {
            AndroidNotificationCenter.CancelAllNotifications();
            ScheduleReminder();
        }
    }

    void CreateChannel()
    {
        var channel = new AndroidNotificationChannel()
        {
            Id = "reminder_channel",
            Name = "Reminder",
            Importance = Importance.Default,
            Description = "แจ้งเตือนให้กลับมาทบทวน",
        };

        AndroidNotificationCenter.RegisterNotificationChannel(channel);
    }

    void ScheduleReminder()
    {
        var notification = new AndroidNotification()
        {
            Title = "อย่าลืมทบทวนนะ!",
            Text = "ถึงเวลาฝึกความจำแล้วค่า กลับมาเล่นได้เลย!",
            FireTime = DateTime.Now.AddMinutes(1),
            //FireTime = DateTime.Now.AddDays(2),
        };

        AndroidNotificationCenter.SendNotification(notification, "reminder_channel");
    }
}