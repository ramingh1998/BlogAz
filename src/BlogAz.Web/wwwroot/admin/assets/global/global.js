function deleteCategory(categoryId) {
	Swal.fire({
		title: 'Əminsinizmi?',
		text: "Bu əməliyyat geri alına bilməz!",
		icon: 'warning',
		showCancelButton: true,
		confirmButtonText: 'Bəli, sil!',
		cancelButtonText: 'İmtina et!',
		reverseButtons: true
	}).then((result) => {
		if (result.isConfirmed) {
			$.ajax({
				url: '/Admin/Category/DeleteCategory',
				type: 'POST',
				data: { id: categoryId },
				success: function (response) {
					console.log(response);
					if (response.status == 200) {
						document.querySelector(`.delete-btn[data-id='${categoryId}']`).closest('.category-item').remove();
						Swal.fire('Silindi!', 'Kategoriya uğurla silindi.', 'success');
					} else {
						Swal.fire('Xəta!', response.message, 'error');
					}
				},
				error: function () {
					Swal.fire('Xəta!', 'Sorğu göndərilməsində problem yarandı.', 'error');
				}
			});
		}
	});
}

function deleteBlog(id) {
	Swal.fire({
		title: 'Bu bloğu silmək istəyirsiniz?',
		text: "Bu əməliyyat geri alına bilməz!",
		icon: 'warning',
		showCancelButton: true,
		confirmButtonText: 'Bəli, sil!',
		cancelButtonText: 'İmtina et!',
		reverseButtons: true
	}).then((result) => {
		if (result.isConfirmed) {
			$.ajax({
				url: '/Admin/Blog/Delete',
				type: 'POST',
				data: { id: id },
				success: function (response) {
					if (response.status == 200) {
						Swal.fire({
							title: 'Əməliyyat uğurlu oldu',
							text: 'Blog uğurla silindi.',
							icon: 'success',
							confirmButtonText: 'Tamam'
						}).then(function (result) {
							location.reload();
						});
					} else {
						Swal.fire({
							title: 'Xəta',
							text: 'Blog Silinərkən xəta baş verdi.',
							icon: 'error',
							confirmButtonText: 'Bəli'
						}).then(function (result) {
							location.reload();
						});
					}
				},
				error: function () {
					Swal.fire('Xəta!', 'Sorğu göndərilməsində problem yarandı.', 'error');
				}
			});
		}
	});
}

$(document).ready(function () {
	var parentId = null;

	$('.add-subcategory-btn').click(function () {
		parentId = $(this).data('id');
	});

	$('#saveSubCategoryBtn').click(function () {
		var title = $('#subcategoryTitle').val();

		if (title.trim() !== "") {
			$.ajax({
				url: '/Admin/Category/AddCategory',
				type: 'POST',
				data: {
					title: title,
					parentId: parentId
				},
				success: function (response) {
					if (response.status == 200) {
						Swal.fire({
							title: 'Əməliyyat uğurlu oldu',
							text: 'Alt kateqoriya uğurla əlavə edildi.',
							icon: 'success',
							confirmButtonText: 'Tamam'
						}).then(function (result) {
							location.reload();
						});
					} else {
						Swal.fire({
							title: 'Xəta',
							text: 'Alt kateqoriya əlavə edərkən xəta baş verdi.',
							icon: 'error',
							confirmButtonText: 'Bəli'
						}).then(function (result) {
							location.reload();
						});
					}
				},
				error: function () {
					Swal.fire({
						title: 'Xəta',
						text: 'Server ilə əlaqə qurularkən xəta baş verdi.',
						icon: 'error',
						confirmButtonText: 'Bəli'
					}).then(function (result) {
						location.reload();
					});
				}
			});
		} else {
			Swal.fire({
				title: 'Xəta',
				text: 'Başlıq boş ola bilməz.',
				icon: 'warning',
				confirmButtonText: 'Bəli'
			});
		}
	});
});


$(document).on('click', '.edit-btn', function (e) {
	e.preventDefault();
	var url = $(this).attr('href'); // URL اکشن

	$.ajax({
		url: url,
		method: 'GET',
		success: function (response) {
			$('#edit .modal-body').html(response); // داده‌ها را درون modal-body قرار دهید
			$('#edit').modal('show'); // مودال را نمایش دهید
		},
		error: function () {
			alert("Xəta baş verdi. Zəhmət olmasa yenidən cəhd edin.");
		}
	});
});

$(document).on('click', '#editCategory', function () {
	var formData = {
		id: $('#categoryTitle').data('id'),
		name: $('#categoryTitle').val()
	};

	$.ajax({
		url: '/Admin/Category/Edit',
		method: 'POST',
		data: JSON.stringify(formData),
		contentType: 'application/json',
		success: function (response) {
			if (response.status == 200) {
				Swal.fire({
					title: 'Əməliyyat uğurlu oldu',
					text: 'Dəyişikliklər uğurla tətbiq edildi.',
					icon: 'success',
					confirmButtonText: 'Tamam'
				}).then(function (result) {
					location.reload();
				});
			} else {
				Swal.fire({
					title: 'Xəta',
					text: 'Xəta baş verdi.',
					icon: 'error',
					confirmButtonText: 'Bəli'
				}).then(function (result) {
					location.reload();
				});
			}
		},
		error: function () {
			Swal.fire({
				title: 'Xəta',
				text: 'Server ilə əlaqə qurularkən xəta baş verdi.',
				icon: 'error',
				confirmButtonText: 'Bəli'
			}).then(function (result) {
				location.reload();
			});
		}
	});
});

