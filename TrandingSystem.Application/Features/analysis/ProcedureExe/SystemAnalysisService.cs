using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrandingSystem.Application.Dtos;
using TrandingSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace TrandingSystem.Application.Features.analysis.ProcedureExe
{
    public interface ISystemAnalysisService
    {
        Task<SystemAnalysisDashboardDto> GetDashboardDataAsync(DateTime? startDate = null, DateTime? endDate = null);
    }

    public class SystemAnalysisService : ISystemAnalysisService
    {
        private readonly db23617Context _context;

        public SystemAnalysisService(db23617Context context)
        {
            _context = context;
        }

        public async Task<SystemAnalysisDashboardDto> GetDashboardDataAsync(DateTime? startDate = null, DateTime? endDate = null)
        {
            var result = new SystemAnalysisDashboardDto
            {
                MonthlyProfits = new List<MonthlyProfitDto>(),
                TopActiveUsers = new List<TopActiveUserDto>(),
                CommunityMessages = new List<CommunityMessagesDto>(),
                RecentNotifications = new List<RecentNotificationDto>()
            };

            // Create parameters
            var startDateParam = new SqlParameter("@StartDate", SqlDbType.DateTime)
            {
                Value = (object)startDate ?? DBNull.Value
            };
            var endDateParam = new SqlParameter("@EndDate", SqlDbType.DateTime)
            {
                Value = (object)endDate ?? DBNull.Value
            };

            // Get database connection
            var connection = _context.Database.GetDbConnection();

            try
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "GetSystemAnalysisDashboard";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(startDateParam);
                    command.Parameters.Add(endDateParam);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        // Result Set 1: System Overview
                        if (await reader.ReadAsync())
                        {
                            result.Overview = new SystemOverviewDto
                            {
                                TotalOrders = reader.GetInt32(0),
                                TotalProfit = reader.GetDecimal(1),
                                TotalUsers = reader.GetInt32(2),
                                TotalMessages = reader.GetInt32(3),
                                PreviousOrders = reader.GetInt32(4),
                                PreviousProfit = reader.GetDecimal(5),
                                PreviousUsers = reader.GetInt32(6),
                                PreviousMessages = reader.GetInt32(7)
                            };
                        }

                        // Result Set 2: Monthly Profit Trend
                        if (await reader.NextResultAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                result.MonthlyProfits.Add(new MonthlyProfitDto
                                {
                                    MonthName = reader.GetString(0),
                                    MonthNumber = reader.GetInt32(1),
                                    Year = reader.GetInt32(2),
                                    MonthlyProfit = reader.GetDecimal(3)
                                });
                            }
                        }

                        // Result Set 3: Top Active Users
                        if (await reader.NextResultAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                result.TopActiveUsers.Add(new TopActiveUserDto
                                {
                                    UserId = reader.GetInt32(0),
                                    FullName = reader.GetString(1),
                                    ActivitiesCount = reader.GetInt32(2),
                                    Status = reader.GetString(3)
                                });
                            }
                        }

                        // Result Set 4: Best User of Month
                        if (await reader.NextResultAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                result.BestUser = new BestUserDto
                                {
                                    UserId = reader.GetInt32(0),
                                    FullName = reader.GetString(1),
                                    TotalMessages = reader.GetInt32(2),
                                    TotalEnrollments = reader.GetInt32(3),
                                    ActivityScore = reader.GetInt32(4)
                                };
                            }
                        }

                        // Result Set 5: Community Statistics
                        if (await reader.NextResultAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                result.CommunityStats = new CommunityStatsDto
                                {
                                    TotalCommunities = reader.GetInt32(0),
                                    MostActiveCommunity = reader.IsDBNull(1) ? null : reader.GetString(1),
                                    AvgMessagesPerCommunity = reader.IsDBNull(2) ? null : reader.GetInt32(2)
                                };
                            }
                        }

                        // Result Set 6: Community Messages
                        if (await reader.NextResultAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                result.CommunityMessages.Add(new CommunityMessagesDto
                                {
                                    CommunityName = reader.GetString(0),
                                    MessageCount = reader.GetInt32(1)
                                });
                            }
                        }

                        // Result Set 7: Recent Notifications
                        if (await reader.NextResultAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                result.RecentNotifications.Add(new RecentNotificationDto
                                {
                                    NotificationId = reader.GetInt32(0),
                                    Subject = reader.GetString(1),
                                    Message = reader.GetString(2),
                                    CreatedAt = reader.GetDateTime(3),
                                    TimeAgo = reader.GetString(4)
                                });
                            }
                        }

                        // Result Set 8: System Health
                        if (await reader.NextResultAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                result.SystemHealth = new SystemHealthDto
                                {
                                    SystemUptimePercent = reader.GetDecimal(0),
                                    ActiveSessions = reader.GetInt32(1),
                                    UsersOnline = reader.GetInt32(2),
                                    LastSystemUpdate = reader.IsDBNull(3) ? null : reader.GetDateTime(3),
                                    PendingOrders = reader.GetInt32(4),
                                    RejectedOrders = reader.GetInt32(5)
                                };
                            }
                        }

                        // Result Set 9: Order Status Summary
                        if (await reader.NextResultAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                result.OrderStatusSummary = new OrderStatusSummaryDto
                                {
                                    AcceptedOrders = reader.GetInt32(0),
                                    RejectedOrders = reader.GetInt32(1),
                                    PendingOrders = reader.GetInt32(2),
                                    TotalOrders = reader.GetInt32(3)
                                };
                            }
                        }

                        // Result Set 10: Course Statistics
                        if (await reader.NextResultAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                result.CourseStats = new CourseStatsDto
                                {
                                    ActiveCourses = reader.GetInt32(0),
                                    ActiveVideos = reader.GetInt32(1),
                                    ActiveLiveSessions = reader.GetInt32(2),
                                    MostPopularCourse = reader.IsDBNull(3) ? null : reader.GetString(3),
                                    MonthlyEnrollments = reader.GetInt32(4)
                                };
                            }
                        }
                    }
                }
            }
            finally
            {
                await connection.CloseAsync();
            }

            return result;
        }
    }
}
