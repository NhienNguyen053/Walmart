function checkform() {
    var regex = /[A-Z]/;
    var regex2 = /[a-z]/;
    var regex3 = /^(?=.*\d)(?=.*[\W_]).+$/;
    var fn = document.getElementById("firstname");
    var ln = document.getElementById("lastname");
    var ps = document.getElementById("password");
    var count = 0;
    if (fn.value == "") {
      document.getElementById("fn1").style.display = "block";
      document.getElementById("fn1").innerHTML = "First name is required "+"<span class='fa-solid fa-circle-exclamation'></span>";
    } else {
      if(/\s/.test(fn.value)){
        document.getElementById("fn1").style.display = "block";
        document.getElementById("fn1").innerHTML = "First name can not have white space "+"<span class='fa-solid fa-circle-exclamation'></span>";
      } else {
        document.getElementById("fn1").style.display = "none";
        count++;
      }        
    }
    if (ln.value == "") {
      document.getElementById("ln1").style.display = "block";
      document.getElementById("ln1").innerHTML = "Last name is required "+"<span class='fa-solid fa-circle-exclamation'></span>";
    } else {
      if(/\s/.test(ln.value)){
        document.getElementById("ln1").style.display = "block";
        document.getElementById("ln1").innerHTML = "Last name can not have white space "+"<span class='fa-solid fa-circle-exclamation'></span>";
      } else {
        document.getElementById("ln1").style.display = "none";
        count++;
      }   
    }
    if (ps.value.length <= 100 && ps.value.length >= 8) {
      document.getElementById("red1").style.display = "none";
      document.getElementById("green1").style.display = "inline-block";
      count++;
    } else {
      document.getElementById("green1").style.display = "none";
      document.getElementById("red1").style.display = "inline-block";
    }
    if (ps.value.match(regex) && ps.value.match(regex2)) {
      document.getElementById("red2").style.display = "none";
      document.getElementById("green2").style.display = "inline-block";
      count++;
    } else {
      document.getElementById("green2").style.display = "none";
      document.getElementById("red2").style.display = "inline-block";
    }
    if (ps.value.match(regex3)) {
      document.getElementById("red3").style.display = "none";
      document.getElementById("green3").style.display = "inline-block";
      count++;
    } else {
      document.getElementById("green3").style.display = "none";
      document.getElementById("red3").style.display = "inline-block";
    }
    if(count == 5){
      document.getElementById("fullname").value = fn.value + " " + ln.value;
      return true;
    }
    else{
      return false;
    }
  }
  function checkform2(){
    var regex = /[A-Z]/;
    var regex1 = /^\d+$/;
    var regex2 = /[a-z]/;
    var regex3 = /^(?=.*\d)(?=.*[\W_]).+$/;
    var regex4 = /^[\w.+\-]+@gmail\.com$/;
    var fn = document.getElementById("firstname");
    var ln = document.getElementById("lastname");
    var pn = document.getElementById("phonenumber");
    var ps = document.getElementById("password");
    var em = document.getElementById("email");
    var count = 0;
    if (fn.value == "") {
      document.getElementById("fn1").style.display = "block";
      document.getElementById("fn1").innerHTML = "First name is required !";
    } else {
      if(/\s/.test(fn.value)){
        document.getElementById("fn1").style.display = "block";
        document.getElementById("fn1").innerHTML = "First name can not have white space !";
      } else {
        document.getElementById("fn1").style.display = "none";
        count++;
      }        
    }
    if (ln.value == "") {
      document.getElementById("ln1").style.display = "block";
      document.getElementById("ln1").innerHTML = "Last name is required !";
    } else {
      if(/\s/.test(ln.value)){
        document.getElementById("ln1").style.display = "block";
        document.getElementById("ln1").innerHTML = "Last name can not have white space !";
      } else {
        document.getElementById("ln1").style.display = "none";
        count++;
      }   
    }
    if (pn.value.match(regex1)){
      document.getElementById("pn1").style.display = "none";
      count++;
    } else if (pn.value == ""){
      document.getElementById("pn1").style.display = "none";
      count++;
    } else{
      document.getElementById("pn1").style.display = "block";
    }
    if (ps.value.length <= 100 && ps.value.length >= 8 && ps.value.match(regex) && ps.value.match(regex2) && ps.value.match(regex3)) {
      document.getElementById("ps1").style.display = "none";
      count++;
    } else {
      document.getElementById("ps1").style.display = "block";
    }
    if (em.value.match(regex4)){
      document.getElementById("em1").style.display = "none";
      count++;
    } else{
      document.getElementById("em1").style.display = "block";
    }
    if(count == 5){
      document.getElementById("fullname").value = fn.value + " " + ln.value;
      return true;
    }
    else{
      return false;
    }
  }
  function checkform3(){
    var count = 0;
    var regex = /^[0-9]+(\.[0-9]+)?$/;
    var regex2 = /^[0-9]+$/;
    var category = document.getElementById("category");
    var subcategory = document.getElementById("subcate");
    var productname = document.getElementById("productname");
    var productprice = document.getElementById("productprice");
    var productquantity = document.getElementById("productquantity");
    var productdescription = document.getElementById("productdescription");
    
    if(category.value == "Select a category"){
      document.getElementById("c1").style.display = "block";
    }else {
      document.getElementById("c1").style.display = "none";
      count++;
    }
    if(subcategory.value == "Select a subcategory"){
      document.getElementById("sc1").style.display = "block";
    }else {
      document.getElementById("sc1").style.display = "none";
      count++;
    }
    if(productname.value.trim() == ""){
      document.getElementById("pn1").style.display = "block";
    }else {
      document.getElementById("pn1").style.display = "none";
      count++;
    }
    if(!productprice.value.match(regex)){
      document.getElementById("pp1").style.display = "block";
      document.getAnimations("pp1").innerHTML = "Product price can only be a number !"
    }else if(productprice.value < 0){
      document.getElementById("pp1").style.display = "block";
      document.getAnimations("pp1").innerHTML = "Product price can't be lower or equal to 0 !"
    }else {
      document.getElementById("pp1").style.display = "none";
      count++;
    }
    if(!productquantity.value.match(regex2)){
      document.getElementById("pq1").style.display = "block";
      document.getAnimations("pq1").innerHTML = "Product quantity can only be a number !"
    }else if(productquantity.value < 0){
      document.getElementById("pq1").style.display = "block";
      document.getAnimations("pq1").innerHTML = "Product quantity can't be lower than 0 !"
    }else {
      document.getElementById("pq1").style.display = "none";
      count++;
    }
    if(productdescription.value.trim() == ""){
      document.getElementById("pd1").style.display = "block";
    }else {
      document.getElementById("pd1").style.display = "none";
      count++;
    }
    if(count == 6){
      return true;
    }else {
      return false;
    }
  }
  function checkform4(){
    var count = 0;
    var regex1 = /^\d+$/;
    var fullname = document.getElementById("fullname");
    var phonenumber = document.getElementById("phonenumber");
    var address = document.getElementById("address");
    if(fullname.value.trim() == ""){
      document.getElementById("fn1").style.display = "block";
    }else {
      document.getElementById("fn1").style.display = "none";
      count++;
    }
    if(phonenumber.value.trim() == ""){
      document.getElementById("pn1").style.display = "block";
      document.getElementById("pn1").innerHTML = "Phone number can not be empty !";
    }else if(!phonenumber.value.match(regex1)) {
      document.getElementById("pn1").style.display = "block";
      document.getElementById("pn1").innerHTML = "Phone number can only contain number !";
    }else {
      document.getElementById("pn1").style.display = "none";
      count++;
    }
    if(address.value.trim() == ""){
      document.getElementById("add1").style.display = "block";
    }else {
      document.getElementById("add1").style.display = "none";
      count++;
    }
    if(count == 3){
      return true;
    }else {
      return false;
    }
  }
  function checkform5(){
    var count = 0;
    var regex1 = /^\d+$/;
    var customername = document.getElementById("customername");
    var phonenumber = document.getElementById("phonenumber");
    var address = document.getElementById("address");
    if(customername.value.trim() == ""){
      document.getElementById("cn1").style.display = "block";
    }else{
      document.getElementById("cn1").style.display = "none";
      count++;
    }
    if(phonenumber.value.trim() == ""){
      document.getElementById("pn1").style.display = "block";
      document.getElementById("pn1").innerHTML = "Phone number can not be empty !";
    }else if(!phonenumber.value.match(regex1)) {
      document.getElementById("pn1").style.display = "block";
      document.getElementById("pn1").innerHTML = "Phone number can only contain number !";
    }else {
      document.getElementById("pn1").style.display = "none";
      count++;
    }
    if(address.value.trim() == ""){
      document.getElementById("add1").style.display = "block";
    }else{
      document.getElementById("add1").style.display = "none";
      count++;
    }
    if(count == 3){
      return true;
    }else{
      return false;
    }
  }
  function checkform6(){
    var count = 0;
    var regex1 = /^\d+$/;
    var firstname = document.getElementById("firstname").value;
    var lastname = document.getElementById("lastname").value;
    var phonenumber = document.getElementById("phonenumber").value;
    if(firstname.trim() == ""){
      document.getElementById("fn1").style.display = "block";
    }else{
      document.getElementById("fn1").style.display = "none";
      count++;
    }
    if(lastname.trim() == ""){
      document.getElementById("ln1").style.display = "block";
    }else{
      document.getElementById("ln1").style.display = "none";
      count++;
    }
    if(phonenumber.trim() == ""){
      document.getElementById("pn1").style.display = "none";
      count++;
    }else if(!phonenumber.match(regex1)) {
      document.getElementById("pn1").style.display = "block";
      document.getElementById("pn1").innerHTML = "Phone number can only contain number !";
    }else {
      document.getElementById("pn1").style.display = "none";
      count++;
    }
    if(count == 3){
      return true;
    }else{
      return false;
    }
  }
  function regex() {
    var regex = /^[\w.+\-]+@gmail\.com$/;
    var email = document.getElementById("email").value;
    if (email == "") {
      document.getElementById("valid").style.display = "block";
      document.getElementById("valid").innerHTML =
        "Email address is required " +
        "<span class='fa-solid fa-circle-exclamation'></span>";
        return false;
    } else if (email.match(regex)) {
      localStorage.setItem("storagename", email);
      return true;
    } else {
      document.getElementById("valid").innerHTML =
      "Please enter a valid email address " +
        "<span class='fa-solid fa-circle-exclamation'></span>";
      document.getElementById("valid").style.display = "block";
      return false;
    }
  }
  function checknull(){
    var password = document.getElementById("password").value;
    if(password == ""){
      document.getElementById("valid").style.display = "block";
      return false;
    }
    else{
      return true;
    }
  }
  function checknull2(){
    var password = document.getElementById("categoryname");
    var catename = password.value.trim();
    if(catename === ""){
      document.getElementById("cn1").style.display = "block";
      return false;
    }else{
      return true;
    }
  }
  const input = document.querySelector('#categoryimage');
  const preview = document.querySelector('#image-preview');
  input.addEventListener('change', () => {
    const file = input.files[0];
    if(file == null){
      preview.innerHTML = "";
      preview.style.display = "none";
      document.getElementById("x").style.display = "none";
    }
    const filesize = file.size;
    if(filesize > 4000000){
      preview.style.display = "none";
      document.getElementById("ln1").style.display = "block";
      document.getElementById("x").style.display = "none";
      document.getElementById("ln1").innerHTML = "File size is too big !";
    }else {
    document.getElementById("ln1").style.display = "none";
    const reader = new FileReader();
    reader.addEventListener('load', () => {
      preview.style.display = "block";
      document.getElementById("x").style.display = "block";
      preview.innerHTML = `<img src="${reader.result}" alt="Image preview" style="width:100%" id="preview">`;
    });
    if (file) {
      reader.readAsDataURL(file);
    }
  }});
  function removeimg(){
    input.value = "";
    preview.style.display = "none";
    document.getElementById("x").style.display = "none";
  }
  function replacewithinput(subcategoryid){
    var check = document.getElementById("checkmark-"+subcategoryid).style.display;
    const textToReplace = document.getElementById(subcategoryid);
    const text = textToReplace.textContent;
    if(check == "none"){
    const input = document.createElement("input");
    input.type = "text";
    input.value = text;
    input.id = subcategoryid;
    input.name = "subcategoryname";
    input.classList.add("form-control");
    input.style.width = "fit-content";
    input.style.marginBottom = "5px";
    document.getElementById("checkmark-"+subcategoryid).style.display = "inline";
    textToReplace.parentNode.replaceChild(input, textToReplace);
    }else {
      const text = textToReplace.value;
      document.getElementById("checkmark-"+subcategoryid).style.display = "none";
      const input = document.createElement("span");
      input.textContent = text;
      input.id = subcategoryid;
      input.style.marginTop = "3px";
      textToReplace.parentNode.replaceChild(input, textToReplace);
    }
  }
  function checknull3(subcategoryid){
    var subname = document.getElementById(subcategoryid).value;
    if(subname.trim() == ""){
      document.getElementById("warn-"+subcategoryid).style.display = "block";
      return false;
    }else {
      return true;
    }
  }
  function changemodal(id){
    document.getElementById("modaltext").innerText = id;
  }
  function changemodal2(id){
    document.getElementById("modaltext2").innerText = id;
  }
  function deleteproduct(){
    var id = document.getElementById("modaltext").innerText;
    $.ajax({
      url: "/Admin/Dashboard/DeleteProduct?id=" + id,
      type: "GET",
      dataType: "json",
      success: function(data){
      if(data == true){
        document.getElementById("alert").style.display = "block";
        document.getElementById("alert").style.backgroundColor = "lightgreen";
        document.getElementById("alerttext").innerText = "Delete product successfully";
        document.getElementById(id).style.display = "none";
      }else{
        document.getElementById("alert").style.display = "block";
        document.getElementById("alert").style.backgroundColor = "rgb(227, 112, 112)";
        document.getElementById("alerttext").innerText = "Product has been in an order. Can't delete product !";
      }
      }
    })
  }
  function checkcategory(event){
    event.preventDefault();
    $.ajax({
      url: "/Admin/Dashboard/CheckCategory",
      type: "GET",
      dataType: "json",
      success: function(data){
      if(data == true){
        window.location.href = event.target.href
      } else{
        document.getElementById("alert").style.display = "block";
        document.getElementById("alert").style.backgroundColor = "rgb(227, 112, 112)";
        document.getElementById("alerttext").innerText = "Please create subcategories before creating a product !";
      }       
      }
    })
  }
  function changerole(){
    var role = document.getElementById('roleSelect').value;
    window.location.href = '/Admin/Dashboard/ManageUsers?role=' + role;
  }
  function changestatus(){
    var status = document.getElementById('statusSelect').value;
    window.location.href = '/Admin/Dashboard/ManageOrders?status=' + status;
  }
  function changecategory(){
    const categoryname = document.getElementById('category');
    const categoryid = categoryname.options[categoryname.selectedIndex].id;
    window.location.href = '/Admin/Dashboard/ManageProducts?categoryid=' + categoryid;
  }
  function changesubcategory(){
    const categoryname = document.getElementById('category');
    const subcategoryname = document.getElementById('subcate');
    const categoryid = categoryname.options[categoryname.selectedIndex].id;
    const subcategoryid = subcategoryname.options[subcategoryname.selectedIndex].id;
    window.location.href = '/Admin/Dashboard/ManageProducts?categoryid=' + categoryid + "&subcategoryid=" + subcategoryid;
  }
  function deletecategory(){
    var id = document.getElementById("modaltext").innerText;
    $.ajax({
      url: "/Admin/Dashboard/DeleteCategory?id=" + id,
      type: "GET",
      dataType: "json",
      success: function(data){
      if(data == true){
        document.getElementById("alert").style.display = "block";
        document.getElementById("alert").style.backgroundColor = "lightgreen";
        document.getElementById("alerttext").innerText = "Delete category successfully";
        document.getElementById("category_" + id).style.display = "none";
      }else{
        document.getElementById("alert").style.display = "block";
        document.getElementById("alert").style.backgroundColor = "rgb(227, 112, 112)";
        document.getElementById("alerttext").innerText = "Category has subcategories. Can't delete category !";
      }
      }
    })
  }
  function deletesubcategory(){
    var id = document.getElementById("modaltext2").innerText;
    $.ajax({
      url: "/Admin/Dashboard/DeleteSubcategory?id=" + id,
      type: "GET",
      dataType: "json",
      success: function(data){
      if(data == true){
        document.getElementById("alert").style.display = "block";
        document.getElementById("alert").style.backgroundColor = "lightgreen";
        document.getElementById("alerttext").innerText = "Delete subcategory successfully";
        document.getElementById("subcategory_" + id).style.display = "none";
      }else{
        document.getElementById("alert").style.display = "block";
        document.getElementById("alert").style.backgroundColor = "rgb(227, 112, 112)";
        document.getElementById("alerttext").innerText = "Subcategory has products. Can't delete subcategory !";
      }
      }
    })
  }
  
