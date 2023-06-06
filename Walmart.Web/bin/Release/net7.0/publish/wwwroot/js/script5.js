const categoryname = document.getElementById('category');
const subcategoryname = document.getElementById('subcate');
  categoryname.addEventListener('change', () => {
    const categoryid = categoryname.options[categoryname.selectedIndex].id;
    $.ajax({
        url: "/Admin/Dashboard/GetSubcategorybyCategory?categoryid=" + categoryid,
        type: "GET",
        dataType: "json",
        success: function(data){
          console.log(data);
          const subcategorySelect = $('#subcate');
          subcategorySelect.empty();
          subcategorySelect.append($('<option>', {
            style: 'display: none;',
            text: 'Select a subcategory'
          }));
          data.forEach(function(subcategory) {
            subcategorySelect.append($('<option>', {
              id: subcategory.subCategoryId,
              text: subcategory.subCategory_Name
            }));
          });
          subcategorySelect.prop('disabled', false);
        }
      });
    });
    const subcategoryname2 = document.getElementById('subcate');
    subcategoryname2.addEventListener('change', () => {
        const subcategoryid = subcategoryname.options[subcategoryname.selectedIndex].id;
        document.getElementById("subcategoryid").value = subcategoryid;
      });