

namespace TrandingSystem.Domain.Helper
{
    public class EmailBody
    {
        public string dir { set; get; }
        public string Subject { set; get; }
        public string StieName { set; get; }
        public string Hi { set; get; }
        public string UserName { set; get; }
        public string info1 { set; get; }
        public string info2 { set; get; }
        public string info3 { set; get; }

        public string contact { set; get; }
        public string ActionUrl { set; get; }
        public string namebtn { set; get; }

    }

    public static class EmailTemplate
    {

        public static string CreateBodyMail(EmailBody temp)
        {
            string _htmlBode = $@"<!doctype html>
<html lang='ar' dir='{temp.dir}'>
<head>
  <meta charset='utf-8'>
  <title>{temp.Subject}</title>
 
</head>
<body style='margin:0;padding:0;font-family:Tahoma,Arial,sans-serif;background:#f6f9fc;'>
  <table width='100%' cellpadding='0' cellspacing='0' style='padding:24px 0;'>
    <tr>
      <td align='center'>
        <table width='600' cellpadding='0' cellspacing='0' style='background:#fff;border-radius:12px;border:1px solid #e9eef5;'>
          <tr>
           <td style='background:#444444;color:white;padding:18px;font-weight:700;'>{temp.StieName}</td>
          </tr>
          <tr>
            <td style='padding:20px;color:#0f172a;'>
              <p style=''>{temp.Hi} {temp.UserName} ,</p>
             
              <p style=''>{temp.info1}</p>
              <p style=''>{temp.info2}</p>
              <p style='color: rgb(60, 60, 220); font-size: 20px;'>{temp.info3}</p>
               <a href='{temp.ActionUrl}' style='display:inline-block;padding:12px 18px;background:#c3972e;color:#fff;border-radius:8px;text-decoration:none;'>
                {temp.namebtn}
              </a>
                <a  href='https://wa.me/201278313491' style='display:inline-block;padding:12px 18px;background:#25D366;color:#fff;border-radius:8px;text-decoration:none;'>
            <i class='bi bi-whatsapp'></i>

                {temp.contact}
              </a>
            </td>
          </tr>
        </table>
      </td>
    </tr>
  </table>
</body>
</html>";

            return _htmlBode;
        }

 


    }
}
