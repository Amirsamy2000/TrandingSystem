using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.Application.Common.Response;
using TrandingSystem.Application.Features.Courses.Commands;
using TrandingSystem.Domain.Entities;
using TrandingSystem.Domain.Interfaces;
using TrandingSystem.Infrastructure.Constants;

namespace TrandingSystem.Application.Features.Courses.Handlers
{
    public class EnrollCourseHandler : IRequestHandler<EnrollCourseCommand, Response<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly IFileService _imageService;

        private readonly INotificationService _notificationService;

        private readonly UserManager<User> _userManager;


        public EnrollCourseHandler(IUnitOfWork unitOfWork, IMapper mapper, IFileService imageService,UserManager<User> userManager,INotificationService notificationService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _imageService = imageService;
            _userManager = userManager;
            _notificationService = notificationService;
        }
        public async Task<Response<bool>> Handle(EnrollCourseCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            try
            {
                var isEnrolled = _unitOfWork.Courses.IsCourseEnrolled(request.CourseId, request.UserId);
                if (isEnrolled)
                {
                    return Response<bool>.ErrorResponse("User is already enrolled in this course");
                }

                string imageUrl = null;
                if (request.ReceiptImage != null)
                {
                    imageUrl = _imageService.SaveImageAsync(request.ReceiptImage, ConstantPath.PathdReceiptsImage).Result;
                }
                
                
                
                
                var result = _unitOfWork.Courses.EnrollCourse(request.CourseId, request.UserId,imageUrl);


                if (result)
                {
                    foreach (var User in _userManager.GetUsersInRoleAsync("Admin").Result.ToList())
                    {

                        await _notificationService.SendEmailAsync(User.Email, "New Order", @$"
                        
                        <!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>New Enrollment Request</title>
    <style>
        * {{
            margin: 0;
            padding: 0;
            box-sizing: border-box;
        }}
        
        body {{
            font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Oxygen, Ubuntu, Cantarell, sans-serif;
            line-height: 1.6;
            color: #333;
            background-color: #f5f5f5;
            padding: 20px;
        }}
        
        .email-container {{
            max-width: 600px;
            margin: 0 auto;
            background: linear-gradient(135deg, #141921 0%, #1a202c 100%);
            border-radius: 12px;
            overflow: hidden;
            box-shadow: 0 20px 40px rgba(20, 25, 33, 0.3);
        }}
        
        .header {{
            background: #141921;
            padding: 30px;
            text-align: center;
            position: relative;
        }}
        
        .header::before {{
            content: '';
            position: absolute;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            background: url('data:image/svg+xml,<svg xmlns=""http://www.w3.org/2000/svg"" viewBox=""0 0 100 100""><circle cx=""20"" cy=""20"" r=""2"" fill=""rgba(255,255,255,0.1)""/><circle cx=""80"" cy=""30"" r=""1.5"" fill=""rgba(255,255,255,0.1)""/><circle cx=""40"" cy=""70"" r=""1"" fill=""rgba(255,255,255,0.1)""/><circle cx=""70"" cy=""80"" r=""2"" fill=""rgba(255,255,255,0.1)""/></svg>') repeat;
            pointer-events: none;
        }}
        
        .header-content {{
            position: relative;
            z-index: 1;
        }}
        
        .header h1 {{
            color: #c3972e;
            font-size: 28px;
            font-weight: 700;
            margin-bottom: 8px;
            text-shadow: 0 2px 4px rgba(0,0,0,0.3);
        }}
        
        .header .subtitle {{
            color: rgba(195, 151, 46, 0.8);
            font-size: 14px;
            font-weight: 500;
            letter-spacing: 0.5px;
            text-transform: uppercase;
        }}
        
        .content {{
            padding: 40px 30px;
            background: white;
            position: relative;
        }}
        
        .notification-badge {{
            display: inline-flex;
            align-items: center;
            background: linear-gradient(45deg, #c3972e, #d4a942);
            color: #141921;
            padding: 8px 16px;
            border-radius: 20px;
            font-size: 12px;
            font-weight: 600;
            text-transform: uppercase;
            letter-spacing: 0.5px;
            margin-bottom: 25px;
        }}
        
        .notification-badge::before {{
            content: '🎓';
            margin-right: 8px;
            font-size: 14px;
        }}
        
        .main-text {{
            font-size: 16px;
            color: #141921;
            margin-bottom: 30px;
            line-height: 1.7;
        }}
        
        .details-card {{
            background: linear-gradient(135deg, rgba(195, 151, 46, 0.05), rgba(195, 151, 46, 0.1));
            border-left: 4px solid #c3972e;
            padding: 20px;
            border-radius: 0 8px 8px 0;
            margin: 25px 0;
        }}
        
        .detail-item {{
            display: flex;
            margin-bottom: 12px;
            align-items: center;
        }}
        
        .detail-item:last-child {{
            margin-bottom: 0;
        }}
        
        .detail-label {{
            font-weight: 600;
            color: #141921;
            min-width: 80px;
            font-size: 14px;
        }}
        
        .detail-value {{
            color: #555;
            font-weight: 500;
        }}
        
        .action-section {{
            background: #f8f9fa;
            padding: 20px;
            border-radius: 8px;
            text-align: center;
            margin: 25px 0;
        }}
        
        .action-text {{
            color: #666;
            font-size: 14px;
            margin-bottom: 15px;
        }}
        
        .cta-button {{
            display: inline-block;
            background: linear-gradient(45deg, #c3972e, #d4a942);
            color: #141921;
            padding: 12px 30px;
            text-decoration: none;
            border-radius: 25px;
            font-weight: 600;
            font-size: 14px;
            text-transform: uppercase;
            letter-spacing: 0.5px;
            transition: all 0.3s ease;
            box-shadow: 0 4px 15px rgba(195, 151, 46, 0.3);
        }}
        
        .cta-button:hover {{
            transform: translateY(-2px);
            box-shadow: 0 8px 25px rgba(195, 151, 46, 0.4);
        }}
        
        .footer {{
            background: #141921;
            padding: 30px;
            text-align: center;
        }}
        
        .footer-text {{
            color: rgba(255, 255, 255, 0.8);
            font-size: 14px;
            margin-bottom: 20px;
        }}
        
        .logo-container {{
            margin-top: 20px;
        }}
        
        .logo {{
            max-width: 150px;
            height: auto;
            opacity: 0.9;
            transition: opacity 0.3s ease;
        }}
        
        .logo:hover {{
            opacity: 1;
        }}
        
        .divider {{
            height: 1px;
            background: linear-gradient(90deg, transparent, #c3972e, transparent);
            margin: 20px 0;
        }}
        
        @media (max-width: 600px) {{
            body {{
                padding: 10px;
            }}
            
            .header {{
                padding: 20px;
            }}
            
            .header h1 {{
                font-size: 24px;
            }}
            
            .content {{
                padding: 25px 20px;
            }}
            
            .footer {{
                padding: 20px;
            }}
        }}
    </style>
</head>
<body>
    <div class=""email-container"">
        <div class=""header"">
            <div class=""header-content"">
                <h1>New Enrollment Request</h1>
                <div class=""subtitle"">Course Registration System</div>
            </div>
        </div>
        
        <div class=""content"">
            <div class=""notification-badge"">
                New Request
            </div>
            
            <p class=""main-text"">
                A new enrollment request has been submitted and requires your attention. Please review the details below and take the necessary actions.
            </p>
            
            <div class=""details-card"">
                <div class=""detail-item"">
                    <span class=""detail-label"">Course:</span>
                    <span class=""detail-value"">{_unitOfWork.Courses.ReadById(request.CourseId).TitleEN}</span>
                </div>
                <div class=""detail-item"">
                    <span class=""detail-label"">Student:</span>
                    <span class=""detail-value"">{_unitOfWork.Users.ReadById(request.UserId).FullName}</span>
                </div>
                
            </div>
            
            <div class=""action-section"">
                <p class=""action-text"">
                    Click below to review and process this enrollment request
                </p>
                <a href=""{ConstantPath.MainUrlSite}/Orders/AllOrders"" class=""cta-button"">Review Request</a>
            </div>
            
            <div class=""divider""></div>
            
            <p style=""color: #666; font-size: 13px; line-height: 1.5;"">
                This is an automated notification from your course management system. 
                Please do not reply to this email directly.
            </p>
        </div>
        
        <div class=""footer"">
            <p class=""footer-text"">
                Best regards,<br>
                <strong style=""color: #c3972e;"">Your Educational Team</strong>
            </p>
            
            <div class=""logo-container"">
                <img src=""{ConstantPath.MainUrlSite}/images/Logo.png"" alt=""Logo"" class=""logo""/>
            </div>
        </div>
    </div>
</body>
</html>
                    ");

                    }

                    return Response<bool>.SuccessResponse(true, "User enrolled successfully");
                }
                return Response<bool>.ErrorResponse("Failed to enroll user in the course");
            }
            catch (Exception ex)
            {
                return Response<bool>.ErrorWithException("An error occurred while processing the request", ex.Message);
            }
        }
    }
}
