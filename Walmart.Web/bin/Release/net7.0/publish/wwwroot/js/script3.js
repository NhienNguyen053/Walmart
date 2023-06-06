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
    $(".delete").on('click', function(){
        $.ajax({
            type: "POST",
            url: 'delete.php',
            data:{product_id: id,},
            success: function(msg) {
            if(msg == "Success"){
                window.location.href = "admin_page.php";
            }
            else{
                alert(msg);
            }
            }
        });
    })
});
var sliderContent = document.getElementById("slider-content");
var prevBtn = document.getElementById("prev-btn");
var nextBtn = document.getElementById("next-btn");
var slideHeight = 89;
var currentSlide = 0;

function showSlide(slideIndex) {
  sliderContent.style.top = "-" + (slideIndex * slideHeight) + "px";
  currentSlide = slideIndex;
}

function prevSlide() {
  if (currentSlide > 0) {
    showSlide(currentSlide - 1);
  }
}

function nextSlide() {
  if (currentSlide < sliderContent.children.length - 5) {
    showSlide(currentSlide + 1);
  }
}

prevBtn.addEventListener("click", prevSlide);
nextBtn.addEventListener("click", nextSlide);