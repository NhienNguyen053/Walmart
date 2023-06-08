function myfunction2(){
    if(document.getElementById("signin").style.display == "block"){
      document.getElementById("signin").style.display = "none";
    }
    else{
      document.getElementById("signin").style.display = "block";
    
    }
  }
  document.addEventListener('click', function clickOutside(event) {
    let get = document.getElementById('signin');
    let get2 = document.getElementById('icon3');
  if(get2.contains(event.target)){
  }
    else if (!get.contains(event.target)) {
    get.style.display = 'none';
    }
  });
  
  function openNav() {
    document.getElementById("mySidebar").style.paddingLeft = "20px";
    document.getElementById("mySidebar").style.width = "320px";
  }
  function closeNav() {
    document.getElementById("mySidebar").style.width = "0";
    document.getElementById("mySidebar").style.paddingLeft = "0"
  }
  document.addEventListener('click', function clickOutside(event) {
    let get = document.getElementById('mySidebar');
    let get2 = document.getElementById('sc1');
  if(get2.contains(event.target)){
  }
    else if (!get.contains(event.target)) {
    get.style.paddingLeft = '0';
    get.style.width = '0';
    }
  });
  window.onclick = function(event)
  {
    if (!document.getElementById('two').contains(event.target)) 
    {
      if(!document.getElementById('one').contains(event.target))
      {
        const nodelist2 = document.querySelectorAll(".dropdown2");
        const nodelist = document.querySelectorAll(".hover1");
        for(j=0;j<nodelist.length;j++){
          nodelist[j].style.display = "none";
        }
        for(i=0;i<nodelist2.length;i++){
          nodelist2[i].style.display = "none";
        }
        var dropdowns = document.getElementsByClassName("button3");
        var i;
        for (i = 0; i < dropdowns.length; i++) 
        {
          var openDropdown = dropdowns[i];
          if (openDropdown.classList.contains('show'))
          {
            openDropdown.classList.remove('show');
          }
        }
      }
    }
  }
  window.onmouseover = function(event){
    const collection = document.getElementsByClassName("five");
    const nodelist = document.querySelectorAll(".hover1");
    const nodelist2 = document.querySelectorAll(".dropdown2")
    for(i=0;i<collection.length;i++){
    if(collection[i].contains(event.target))
    {
      for(j=0;j<nodelist.length;j++){
        for(k=0;k<nodelist2.length;k++){
          nodelist2[k].style.display = "none";
        }
        nodelist[j].style.display = "none";
        nodelist2[i].style.display = "flex";
        nodelist[i].style.display = "flex";
      }
    }
  }
  }
  let slideIndex = 0;
    let timeoutId = null;
    const slides = document.getElementsByClassName("mySlides");
    const dots = document.getElementsByClassName("dot");
    showSlides();
  function currentSlide(index) {
      slideIndex = index;
      showSlides();
    }
      function plusSlides(step) {
        if(step < 0) {
        slideIndex -= 2;
        if(slideIndex < 0) {
          slideIndex = slides.length - 1;
        }
    }
    showSlides();
  }
  function showSlides() {
      for(let i = 0; i < slides.length; i++) {
        slides[i].style.display = "none";
        dots[i].classList.remove('active');
      }
      slideIndex++;
      if(slideIndex > slides.length) {
        slideIndex = 1
      }
      slides[slideIndex - 1].style.display = "block";
      dots[slideIndex - 1].classList.add('active');
      if(timeoutId) {
          clearTimeout(timeoutId);
      }
      timeoutId = setTimeout(showSlides, 5000); // Change image every 5 seconds
  }
  