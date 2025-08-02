


        // For Block Video
    function BlockVideo(id, status) {
        let response=status==true?"@Localizer["titleBlock1"]":"@Localizer["titleBlock"]"
    debugger;
    let model = {
        title: response,
    icon: "warning",
    confirmButtonText: "@Localizer["confirmButtonTextBlock"]",
    cancelButtonText: "@Localizer["cancelButtonTextBlock"]",
    url:  `/Videos/BlockVideo?VideoId=${id}&Status=${status}`,
    method: "POST",

    responseSuccess: "@Localizer["responseSuccessBlock"]",
    responseDanger: "@Localizer["responseDangerBlock"]"
            };
    DrawAlter(model);
        }

    // For Delet Videos
    function DeleteVideo(id) {
            debugger;
    let model = {
        title: "@Localizer["titleDelete"]",
    icon: "question",
    confirmButtonText: "@Localizer["confirmButtonTextBlock"]",
    cancelButtonText: "@Localizer["cancelButtonTextBlock"]",
    url:  `/Videos/DeleteVideo?VideoId=${id}`,
    method: "POST",

    responseSuccess: "@Localizer["responseSuccessBlock"]",
    responseDanger: "@Localizer["responseDangerBlock"]"
            };
    DrawAlter(model);
        }

    // For Delet Videos
    function DeleteAllVideos(id) {
            debugger;
    let model = {
        title: "@Localizer["titleDeleteall"]",
    icon: "error",
    confirmButtonText: "@Localizer["confirmButtonTextBlock"]",
    cancelButtonText: "@Localizer["cancelButtonTextBlock"]",
    url:  `/Videos/DeleteAllVideos?CourseId=${id}`,
    method: "POST",

    responseSuccess: "@Localizer["responseSuccessBlock"]",
    responseDanger: "@Localizer["responseDangerBlock"]"
            };
    DrawAlter(model);
        }



    // Dispal Patial Add New Video
    function DispalPatialAddNewVideo(){
        let setting={

        title:"@Localizer["loading"]"
            }
    SweatAlterProcessing(setting)
    $.ajax({
        url:"/Videos/PartialViewAddNewVideo",
    type:"GET",
    success:function(data){
        Swal.close();
    $("#modalbody").html(data)
    $("#mainPartial").removeClass('col-10')
    $("#mainPartial").addClass('col-12')
    // استخدم كود Bootstrap 5 لعرض المودال
    var myModal = new bootstrap.Modal(document.getElementById('largeModal'), {
        backdrop: 'static',     // يمنع الإغلاق عند الضغط على الخلفية, // أو static لو عايز تمنع إغلاقه بالضغط على الخلفية
    keyboard: false      // يسمح بإغلاقه بزر Esc
        });
    myModal.show();
                }

            })
        }

    // Dispal Patial Update New Video
    function DispalPatialUppdateNewVideo(id){

        let setting={

        title:"@Localizer["loading"]"
            }
    SweatAlterProcessing(setting)

    $.ajax({
        url:"/Videos/PartialViewUpdateNewVideo",
    type:"GET",
    data:{
        CourseId:@Coure.CourseId,
    VideId:id

                },
    success:function(data){
        $("#modalbody").html(data)
                    $("#mainPartial").removeClass('col-10')
    $("#mainPartial").addClass('col-12')
    // استخدم كود Bootstrap 5 لعرض المودال
    var myModal = new bootstrap.Modal(document.getElementById('largeModal'), {
        backdrop: 'static',     // يمنع الإغلاق عند الضغط على الخلفية, // أو static لو عايز تمنع إغلاقه بالضغط على الخلفية
    keyboard: false      // يسمح بإغلاقه بزر Esc
        });
    myModal.show();
                },
           


            })
        }
    // For Dipaly Video As Youtube
    function DisplayVideo(id) {
        $.ajax({
            url: "/Videos/DisplayVideo",
            type: "GET",
            data: { VideoId: id }, // ✅ كانت ناقصة =
            success: function (res) {
                console.log("====================================================")
                console.log(res)
                // تأكد أن res.Data يحتوي على رابط فيديو صالح
                $('#UrlVideo').attr('src', res.data);

                // عرض المودال
                var myModal = new bootstrap.Modal(document.getElementById('youTubeModal'));
                myModal.show();
            },
            error: function (res) {
                console.error("Error loading video", res);
            }
        });
        }
    // For Change Is Paid
    function handleIsPaidChange(cost) {
            if (!$("#Ispaid").is(":checked")) {
        // لو مش مدفوع، خليه 0.00
        $("#Cost").val("0.00");
            } else {
        // لو مدفوع، خليه القيمة الأصلية (مثلاً 30.00)
        let normalizedCost = parseFloat(cost).toFixed(2).replace(",", ".");
    $("#Cost").val(normalizedCost);
            }
        }



    function SubmitUpdateVideo() {
                   debugger
    Swal.fire({
        title: 'Processing...',
    allowOutsideClick: false,
                didOpen: () => {
        Swal.showLoading();
                }
            });

    var form = $('#updateVideoForm')[0];
    var data = new FormData(form);

    // أضف الـ token يدويًا من الحقل المخفي
    var token = $('input[name="__RequestVerificationToken"]').val();

    $.ajax({
        type: "POST",
    url: "/Videos/SubmitUpdateVideo",
    data: data,
    processData: false,
    contentType: false,
    headers: {
        'RequestVerificationToken': token
                },
    success: function (res) {
        Swal.close();
    if (res.success) {
        Swal.fire('✔️ Success!', res.message, 'success');
                    } else {
        Swal.fire('❌ Error!', res.message, 'error');
                    }
                },
    error: function (err) {
        Swal.close();
    Swal.fire('❌ Error!', 'Something went wrong', 'error');
                }
            });
        }


