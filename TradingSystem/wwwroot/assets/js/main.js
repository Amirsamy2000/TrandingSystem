// When Click in Btn Dispaly Videos Show Modal For Choice Cousre id Then Move To Display Videos

//const { debug } = require("console");

//const { functionsIn } = require("lodash");

function DisplayCourse() {
    $.ajax({
        url: '/Course/DisplayCourse',
        type: 'GET',
        success: function (data) {
            $('#courseModalBody').html(data);
            $('#courseModal').modal('show');
        },
        error: function (xhr, status, error) {
            console.error('Error fetching courses:', error);
            alert('An error occurred while fetching courses. Please try again later.');
        }
    });
}

// for Draw Alter Dynmic Sweat Alte
// i create this Sweat Alter For Confrimation for Delete And Delete All And Block and handle Appear Response
//and this param ModelSetting

//   let ModelSetting = {
//title: "@Localizer["titleDelete"]",
//icon: "question",
//confirmButtonText: "@Localizer["confirmButtonTextBlock"]",
//cancelButtonText: "@Localizer["cancelButtonTextBlock"]",
//url: `/Videos/DeleteVideo?VideoId=${id}`,
//method: "POST",
//responseSuccess: "@Localizer["responseSuccessBlock"]",
//responseDanger: "@Localizer["responseDangerBlock"]"
//};
function DrawAlter(ModelSetting) {
    Swal.fire({
        title: ModelSetting.title,
        icon: ModelSetting.icon,
        showCancelButton: true,
        confirmButtonText: ModelSetting.confirmButtonText,
        cancelButtonText: ModelSetting.cancelButtonText,
    }).then((result) => {
        if (result.isConfirmed) {
            fetch(ModelSetting.url, { method: ModelSetting.method })
                .then(response => {
                    if (!response.ok) throw new Error("HTTP error " + response.status);
                    return response.json(); // توقعنا Response<T>
                })
                .then(res => {
                    if (res.success) {
                        Swal.fire(res.message, '', 'success').then(() => {
                            location.reload(); // ✅ reload after user clicks "OK" in success alert
                        });;
                    } else {
                        Swal.fire(res.message, '', 'error');
                    }
                })
                .catch(error => {
                    console.error("Error:", error);
                    Swal.fire("Error while connecting to server", '', 'error');
                });
        }
    });
}


// Sweat Alter For Success Response

function SweatAlterSuccess(title) {
    Swal.fire({
        icon: "success",
        title:title,
       
    }).then(() => {
       
        location.reload();
    });
}

// Sweat Alter For failure Response
function SweatAlterDanger(title) {
    Swal.fire({
        icon: "error",
        title:title,
        
    });
}

// Sweat Alter For Processing Response
function SweatAlterProcessing(SweatModel) {
    Swal.fire({
        title: SweatModel.title,
      
        allowOutsideClick: false,
        allowEscapeKey: false,
        didOpen: () => {
            Swal.showLoading();
        }
    });

}


function GeneralRequiredFiled(id, messages, idspanVaildation) {
     
    //const input = $('#' + id);

    ////// تأكد أن العنصر موجود
    ////if (input.length === 0) {
    ////    console.warn(`Input element with ID '${id}' not found.`);
    ////    return false;
    ////}

    const value = $("#"+id).val();

    if (!value || value.trim() === '') {
        $("#" + idspanVaildation).text(messages);
        return false;
    } else {
        $("#" + idspanVaildation).text("");
        return true;
    }
}


function validateImageFile(id, errorSpanId, messagge) {
     
    const input = document.getElementById(id);
    const file = input.files[0];

    if (!file) {
        $("#" + errorSpanId).text(messagge);
        return false;
    }

    const allowedTypes = ['image/jpeg', 'image/png', 'image/gif', 'image/webp'];

    if (!allowedTypes.includes(file.type)) {
        $("#" + errorSpanId).text(messagge);
        input.value = ''; // امسح الملف المختار
        return false;
    } else {
        $("#" + errorSpanId).text('');
        return true;
    }
}

function validateCost(messagePaid, idIspaid, idCost,  idspanerror, messageFree,mess) {
   debugger
    let isPaid = $('#' + idIspaid).is(':checked');
    let costInput = $('#' + idCost);
    let cost = parseFloat(costInput.val());
    let errorSpan = $('#' + idspanerror);

    //if (isNaN(cost)) {
    //    cost = 0;
    //    costInput.val(0);
    //}
    if (cost < 0) {
        errorSpan.text(messageFree);
        return false;
    }
    if ((isPaid && cost <= 0) || (isPaid&&isNaN(cost))) {
        // لو الفيديو مدفوع ولازم يكون فيه تكلفة أكبر من 0
        errorSpan.text(mess);
        return false;
    }

    if (!isPaid && cost > 0) {
        // لو الفيديو مجاني وما ينفعش يكون فيه تكلفة
        errorSpan.text(messagePaid);
        return false;
    }

    errorSpan.text(""); // لا يوجد أخطاء
    return true;
}



function validateVideoFile(inputId, spanValidationId, message) {
     
    const input = document.getElementById(inputId);
    const errorSpan = document.getElementById(spanValidationId);

    const file = input.files[0];
    const allowedTypes = ['video/mp4', 'video/webm', 'video/ogg'];
    const maxSizeMB = 100;

    if (!file) {
        errorSpan.innerText = message;
        return false;
    }

    if (!allowedTypes.includes(file.type)) {
        errorSpan.innerText = message;
        input.value = '';
        return false;
    }

  

    errorSpan.innerText = '';
    return true;
}

function handleIsPaidChangeInAddNewVideo(idpaid, idcost) {
    
    if (!$("#" + idpaid).is(":checked")) {
        // لو مش مدفوع، خليه 0.00
        $("#" + idcost).val("0");
        return true;

    }
 
    else {
        return true;
    }
}
// Before Send Data Of new Video check vaildation


// Show Pop To Choice Cousre then Dispaly  videos
function ChoiceCourse(text,text2) {
    debugger
    $.ajax({
        url: '/Course/ReadAll',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            if (data.success) {
                console.log("-----------------")
                console.log(data)
                console.log(data.data)
                allCourses = data.data;

                let html = `
                <div class="row m-2">
                    <div class="col mb-3">
                       
                        <select id="courseSelect" class="form-select" >`

                // loop through allCourses to add options
                allCourses.forEach(course => {
                    html += `<option value="${course.courseId}">${course.titleEN}</option>`;
                });

                html += `</select>
                    </div>
                    <div class='col-12'>  <button type="button" class="btn btn-outline-info" onclick="GoToPageDiplayVideos()"> ${text} </button>
                     </div>
                     </div>`;           
                


                // Fit Model
                $('#ChoiceCourse').html(html);
                $("#modalCenterTitle").text(text2);
                var myModal = new bootstrap.Modal(document.getElementById('modalCenter'));
                myModal.show();
               
             
            }
            else {
               
                Swal.fire({
                    icon: 'error',
                    title: 'Error',
                    text: data.message,
                    confirmButtonColor: '#696cff'
                });
            }

        },
        error: function (xhr, status, error) {
           
            Swal.fire({
                icon: 'error',
                title: 'Error',
                text: 'Failed to load courses. Please try again.',
                confirmButtonColor: '#696cff'
            });
        }
    });
}


/// Go To Page Display Videos
function GoToPageDiplayVideos() {
    debugger
    window.location.href = '/Videos/Videos?CourseId=' + $("#courseSelect").val();
}














