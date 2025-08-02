// When Click in Btn Dispaly Videos Show Modal For Choice Cousre id Then Move To Display Videos

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




