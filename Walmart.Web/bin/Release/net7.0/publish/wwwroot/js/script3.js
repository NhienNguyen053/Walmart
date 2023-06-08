$(document).ready(function() {
    var id;
    $("#two").on('click', function(){
        $('#one').toggleClass('show');
    })
    $("#sidebar-collapse").on('click', function() {
        $('#sidebar').toggleClass('small-screen');
        $('.sidebar-header').toggleClass('small-screen2');
        $('#content').toggleClass('small-screen3');
        $('.main-content').toggleClass('small-screen3');
    });
    $(".nav-item").on('click',function(){
        var t = $(this).attr('id');
        $(".nav-item:not(#"+t+")").removeClass('active');
        $("#"+t).toggleClass('active');
        $('#'+t+' > a').css('color', 'white');
    })
    $(".sb").on('click', function(){
        var t = $(this).attr('id');
        $(".sb").removeClass('active');
        $("#"+t).toggleClass('active');
    })
    $(".storeid").on('click', function(){
        id = $(this).attr('id');
        $('#pid').attr('value', id);
    })
    $("#edit_fn").on('click', function(){
      $('#fn').css('display', 'none');
      $('#form_fn').css('display', 'block');
    })
    $("#cancel_fn").on('click', function(){
      $('#form_fn').css('display', 'none');
      $('#fn').css('display', 'block');
    })
    $("#edit_em").on('click', function(){
      $('#em').css('display', 'none');
      $('#form_em').css('display', 'block');
    })
    $("#cancel_em").on('click', function(){
      $('#form_em').css('display', 'none');
      $('#em').css('display', 'block');
    })
    $("#edit_pn").on('click', function(){
      $('#pn').css('display', 'none');
      $('#form_pn').css('display', 'block');
    })
    $("#cancel_pn").on('click', function(){
      $('#form_pn').css('display', 'none');
      $('#pn').css('display', 'block');
    })
    $("#edit_add").on('click', function(){
      $('#add').css('display', 'none');
      $('#form_add').css('display', 'block');
    })
    $("#cancel_add").on('click', function(){
      $('#form_add').css('display', 'none');
      $('#add').css('display', 'block');
    })
    $("#edit_pw").on('click', function(){
      $('#pw').css('display', 'none');
      $('#form_pw').css('display', 'block');
    })
    $("#cancel_pw").on('click', function(){
      $('#form_pw').css('display', 'none');
      $('#pw').css('display', 'block');
    })
});
var sliderContent = document.getElementById("slider-content");
var prevBtn = document.getElementById("prev-btn");
var nextBtn = document.getElementById("next-btn");
var slideHeight = 99;
var slideWidth = 80;
var currentSlide = 0;

function showSlide(slideIndex) {
  sliderContent.style.top = "-" + (slideIndex * slideHeight) + "px";
  currentSlide = slideIndex;
}
function showSlide2(slideIndex) {
  sliderContent.style.left = "-" + (slideIndex * slideWidth) + "px";
  currentSlide = slideIndex;
}

function prevSlide() {
  if (currentSlide > 0) {
    if(window.innerWidth > 761){
    showSlide(currentSlide - 1);
    }else {
    showSlide2(currentSlide - 1)
    }
  }
}
window.addEventListener('resize', function() {
  var viewportWidth = window.innerWidth || document.documentElement.clientWidth;

  if (viewportWidth <= 768) {
    slideCount = 3;
  } else {
    slideCount = 5;
  }
});
function nextSlide() {
  if (currentSlide < sliderContent.children.length - 5) {
    if(window.innerWidth > 761){
      showSlide(currentSlide + 1);
      }else {
      showSlide2(currentSlide + 1)
      }
  }
}
prevBtn.addEventListener("click", prevSlide);
nextBtn.addEventListener("click", nextSlide);
function changeimg(path, count){
  document.getElementById("currentimg").src = "/" + path;
  var lines = document.querySelector('.line1');
  var lines2 = document.getElementsByClassName("line2");
  for (var i = 0; i < lines2.length; i++) {
    lines2[i].style.visibility = "hidden";
  }
  lines.style.visibility = "hidden";
  document.getElementById(count).style.visibility = "visible";
}
function smallscreen(){
  var prev = document.getElementById("prev-btn");
  var next = document.getElementById("next-btn");
  if(window.innerWidth <= 761){
    prev.classList.remove("fa-chevron-up");
    prev.classList.add("fa-chevron-left");
    next.classList.remove("fa-chevron-down");
    next.classList.add("fa-chevron-right");
  }else {
    prev.classList.remove("fa-chevron-left");
    prev.classList.add("fa-chevron-up");
    next.classList.remove("fa-chevron-right");
    next.classList.add("fa-chevron-down");
  }
}
window.onresize = smallscreen;
function checkcart(event) {
  event.preventDefault();
  $.ajax({
    url: "/Customer/Home/CheckCart",
    type: "GET",
    dataType: "json",
    success: function(data){
      console.log(data);
      if(data.length === 0){
        window.location.href = event.target.href
      }else {
        data.forEach(element => {
          document.getElementById("valid_"+element).style.display = "block";
        });
      }
    }
  })
};
function changemodal(id){
  document.getElementById("modaltext").innerText = id;
}
function cancelorder(){
  var id = document.getElementById("modaltext").innerText;
    $.ajax({
      url: "/Customer/Home/CancelOrder?id=" + id,
      type: "GET",
      dataType: "json",
      success: function(data){
      if(data == true){
        document.getElementById("alert").style.display = "block";
        document.getElementById("alert").style.backgroundColor = "lightgreen";
        document.getElementById("alerttext").innerText = "Cancel order successfully !";
        document.getElementById("cancel_" + id).style.display = "none";
        document.getElementById("status_" + id).style.backgroundColor = "red";
        document.getElementById("status_" + id).innerText = "Canceled";
      }else{
        document.getElementById("alert").style.display = "block";
        document.getElementById("alert").style.backgroundColor = "rgb(227, 112, 112)";
        document.getElementById("alerttext").innerText = "Can't cancel order !";
      }
      }
    })
}
function checkfn(){
  var count = 0;
  var firstname = document.getElementById("fn1").value.trim();
  var lastname = document.getElementById("ln1").value.trim();
  if(firstname == ""){
    document.getElementById("fn2").style.display = "block";
  }else{
    document.getElementById("fn2").style.display = "none";
    count++;
  }
  if(lastname == ""){
    document.getElementById("ln2").style.display = "block";
  }else{
    document.getElementById("ln2").style.display = "none";
    count++;
  }
  if(count == 2){
    return true;
  }else {
    return false;
  }
}
function checkpn(){
  var count = 0;
  var regex = /^\d+$/;
  var phonenumber = document.getElementById("pn1").value.trim();
  if(phonenumber.match(regex)){
    document.getElementById("pn2").style.display = "none";
    count++;
  }else{
    document.getElementById("pn2").style.display = "block";
  }
  if(count == 1){
    return true;
  }else {
    return false;
  }
}
function checkem(event){
  event.preventDefault();
  var count = 0;
  var regex = /^[\w.+\-]+@gmail\.com$/;
  var email = document.getElementById("em1").value.trim();
  if(email == ""){
    document.getElementById("em2").style.display = "block";
    document.getElementById("em2").innerHTML = "Email can't be empty !";
  }else if(email.match(regex)){
    document.getElementById("pn2").style.display = "none";
    count++;
  }else{
    document.getElementById("em2").style.display = "block";
    document.getElementById("em2").innerHTML = "Invalid email. Please try again !";
  }
  if(count == 1){
    $.ajax({
      url: "/Customer/Home/CheckEmailExist?email=" + email,
      type: "GET",
      dataType: "json",
      success: function(data){
      if(data == true){
        document.getElementById("em2").style.display = "none";
        event.target.submit();
      }else{
        document.getElementById("em2").style.display = "block";
        document.getElementById("em2").innerHTML = "Email already exist. Please try again !";
      }
      }
    })
  }else {
  }
}
function changestatus(){
  var status = document.getElementById('statusSelect').value;
  window.location.href = '/Customer/Home/PurchaseHistory?status=' + status;
}
function checkpw(event){
  event.preventDefault();
  var regex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*\W).{8,100}$/;
  var oldpassword = document.getElementById("pw1").value.trim();
  var newpassword = document.getElementById("pw3").value.trim();
  var alert1 = document.getElementById("pw2");
  var alert2 = document.getElementById("pw4");
  if(oldpassword == "" && newpassword == ""){
    alert1.style.display = "block";
    alert1.innerHTML = "Password can't be empty !";
    alert2.style.display = "block";
    alert2.innerHTML = "Password can't be empty !";
  }else if(oldpassword == "" && newpassword != ""){
    alert1.style.display = "block";
    alert1.innerHTML = "Password can't be empty !";
    alert2.style.display = "none";
  }else if(oldpassword != "" && newpassword == ""){
    alert1.style.display = "none";
    alert2.innerHTML = "Password can't be empty !";
    alert2.style.display = "block";
  }else{
    alert1.style.display = "none";
    alert2.style.display = "none";
    if(newpassword.match(regex)){
      $.ajax({
        url: "/Customer/Home/EditPassword?oldpassword=" + oldpassword + "&newpassword=" + newpassword,
        type: "GET",
        dataType: "json",
        success: function(data){
        if(data == true){
          event.target.submit();
        }else{
          alert1.style.display = "block";
          alert1.innerHTML = "Incorrect password. Please try again !"
        }
        }
      })
    }else {
      alert2.style.display = "block";
      alert2.innerHTML = "Password must include: <br> + 8-100 characters <br> + Upper & lowercase letters <br> + At least one number and special character"
    }
  }
}