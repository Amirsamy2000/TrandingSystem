using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrandingSystem.Application.Dtos
{
    public class SystemOverviewDto
    {
        public int TotalOrders { get; set; }
        public decimal TotalProfit { get; set; }
        public int TotalUsers { get; set; }
        public int TotalMessages { get; set; }
        public int PreviousOrders { get; set; }
        public decimal PreviousProfit { get; set; }
        public int PreviousUsers { get; set; }
        public int PreviousMessages { get; set; }

        // Calculated properties
        public double OrdersChangePercent => PreviousOrders > 0
            ? Math.Round(((double)(TotalOrders - PreviousOrders) / PreviousOrders) * 100, 1)
            : 0;

        public double ProfitChangePercent => PreviousProfit > 0
            ? Math.Round(((double)(TotalProfit - PreviousProfit) / (double)PreviousProfit) * 100, 1)
            : 0;

        public double UsersChangePercent => PreviousUsers > 0
            ? Math.Round(((double)(TotalUsers - PreviousUsers) / PreviousUsers) * 100, 1)
            : 0;

        public double MessagesChangePercent => PreviousMessages > 0
            ? Math.Round(((double)(TotalMessages - PreviousMessages) / PreviousMessages) * 100, 1)
            : 0;
    }

    // Result Set 2: Monthly Profit Trend
    public class MonthlyProfitDto
    {
        public string MonthName { get; set; }
        public int MonthNumber { get; set; }
        public int Year { get; set; }
        public decimal MonthlyProfit { get; set; }
    }

    // Result Set 3: Top Active Users
    public class TopActiveUserDto
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public int ActivitiesCount { get; set; }
        public string Status { get; set; }
    }

    // Result Set 4: Best User of Month
    public class BestUserDto
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public int TotalMessages { get; set; }
        public int TotalEnrollments { get; set; }
        public int ActivityScore { get; set; }
    }

    // Result Set 5: Community Statistics
    public class CommunityStatsDto
    {
        public int TotalCommunities { get; set; }
        public string MostActiveCommunity { get; set; }
        public int? AvgMessagesPerCommunity { get; set; }
    }

    // Result Set 6: Messages by Community
    public class CommunityMessagesDto
    {
        public string CommunityName { get; set; }
        public int MessageCount { get; set; }
    }

    // Result Set 7: Recent Notifications
    public class RecentNotificationDto
    {
        public int NotificationId { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
        public string TimeAgo { get; set; }
    }

    // Result Set 8: System Health
    public class SystemHealthDto
    {
        public decimal SystemUptimePercent { get; set; }
        public int ActiveSessions { get; set; }
        public int UsersOnline { get; set; }
        public DateTime? LastSystemUpdate { get; set; }
        public int PendingOrders { get; set; }
        public int RejectedOrders { get; set; }
    }

    // Result Set 9: Order Status Summary
    public class OrderStatusSummaryDto
    {
        public int AcceptedOrders { get; set; }
        public int RejectedOrders { get; set; }
        public int PendingOrders { get; set; }
        public int TotalOrders { get; set; }
    }

    // Result Set 10: Course Statistics
    public class CourseStatsDto
    {
        public int ActiveCourses { get; set; }
        public int ActiveVideos { get; set; }
        public int ActiveLiveSessions { get; set; }
        public string MostPopularCourse { get; set; }
        public int MonthlyEnrollments { get; set; }
    }

    // Combined Dashboard Data
    public class SystemAnalysisDashboardDto
    {
        public SystemOverviewDto Overview { get; set; }
        public List<MonthlyProfitDto> MonthlyProfits { get; set; }
        public List<TopActiveUserDto> TopActiveUsers { get; set; }
        public BestUserDto BestUser { get; set; }
        public CommunityStatsDto CommunityStats { get; set; }
        public List<CommunityMessagesDto> CommunityMessages { get; set; }
        public List<RecentNotificationDto> RecentNotifications { get; set; }
        public SystemHealthDto SystemHealth { get; set; }
        public OrderStatusSummaryDto OrderStatusSummary { get; set; }
        public CourseStatsDto CourseStats { get; set; }
    }
}
