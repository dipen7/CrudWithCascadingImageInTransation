(function($) {
  "use_strict";
  
  $(window).on('load', function() {
    $('#page-loader').fadeOut('fast', function() {
        $(this).remove();
    });

  });
  
  $(document).ready(function(){

    
/**********************\
// Submit form data using ajax
/************************/

  $('.form-contact').validator().on('submit', function (e) {
    if (e.isDefaultPrevented()) {
      return false;
    } else {
      e.preventDefault();
      $.ajax({
        url:'form-process.php',
        type:"POST",
        data:$('form').serialize(),
        success: function(){
          $('.form-send-callback').html('<span class="lnr lnr-checkmark-circle"></span> Message Sent Successfully').css({
            color:'#72c02c',
            marginTop:"10px",
            fontWeight:"700"
          }).fadeOut(5000);
          $('.form-contact .form-control').val("");
        }
      });
    }
  });
    
    
/*
====================> 
Mailchimp init  
====================>
*/
  $('.newsletter').ajaxChimp({
      callback: callbackFunction,
      url: "http://theme-valley.us14.list-manage.com/subscribe/post?u=00195c0c5d470b3a5759cd322&amp;id=a58d202046" 
  }); 
  function callbackFunction(resp) {
      console.log(resp);
      $("#mc-email").removeClass("error");
      if (resp.result === 'success') {
          $(".subscribe-message").html('<i class="fa fa-check"></i> We have sent you a confirmation email.').fadeIn().css("color", "#72c02c");
      } else {
          var msg = resp.msg.split("-")[1];
          $(".subscribe-message").html('<i class="fa fa-warning"></i> Please enter a valid email').fadeIn().css("color", "red");
          $("#mc-email").addClass("error");
      }
  }
   
    
    
       
    /**********************\
    // nav toggler
    /************************/
       $('.navigation-toggle').on('click', function(){
            $(this).toggleClass('nav__toggle');
           if ($('.sidenav').hasClass('open')) {
                $('.sidenav').css('right', '-280px');
                $('body, #nav > .container').removeClass('left-offset');
                $('.sidenav').removeClass('open');
            }
            else{
                $('.sidenav').css('right', '-1px');
                $('body, #nav > .container').addClass('left-offset');
                $('.sidenav').addClass('open');
            }
        });

      $("#nav .nav-link").on('click', function(){
        $('.navbar-collapse').removeClass("show");
      });

      /**********************\
      //typist init
      /************************/

      if (typeof Typist === "function") {
        new Typist(document.querySelector(".typelist-skill"), {
            letterInterval: 60,
            textInterval: 1000
        });
      }


      /**********************\
      //intro video popup 
      /************************/
        $('.btn-play-video').magnificPopup({
          disableOn: 700,
          type: 'iframe',
          mainClass: 'mfp-fade',
          removalDelay: 160,
          preloader: false,
          fixedContentPos: false
        });


      /**********************\
      // Awards slider Init
      /************************/
      $('.awards-slides').owlCarousel({
        responsiveClass:true,
        responsive : {
          0 : {
            items:1
          },
          576 : {
            items:2
          },    
          768 : {
              items:3
          }
      }
      });


      /**********************\
      // quote slider Init
      /************************/
      $('.quote-slides').owlCarousel({
        responsiveClass:true,
        autoplay:true,
        responsive : {
          0 : {
            items:1
          },
          767: {
            items:1
          },    
          991: {
              items:2
          }
      }
      });


    
      /**********************\
      // skill Progrss-bar
      /************************/
    
    $(window).scroll(function() {
      
       if ($(this).scrollTop() > (($('#skills').offset().top - 300)+ ($('#skills').outerHeight()) - ($(window).height()))){
         
          $(".progress-bar").each(function(){
              each_bar_width = $(this).attr('aria-valuenow');
              $(this).css({
                width:each_bar_width + "%"
              });
              $(this).html('<span class="progress-tooltip" style="left:'+ each_bar_width +'%">' + each_bar_width + '%</span>');
          });
         
       }
      
    });
    



      /**********************\
      //Portfolio gallary
      /************************/
      $('.portfolio').magnificPopup({
        delegate: 'a.popup-img',
        type: 'image',
        closeOnContentClick: false,
        closeBtnInside: false,
        mainClass: 'mfp-with-zoom mfp-img-mobile',
        gallery: {
        enabled: true
        }
      });


      /**********************\
      // Portfolio image slider init
      /************************/
        $('.portfolio-slider').owlCarousel({
        singleItem: true,
        loop: true,
        nav: true,
        items: 1,
        pagination: false,
        dots:false,
        navSpeed:600,
        nav:true,
        navText:['<span class="lnr lnr-arrow-left"></span>', '<span class="lnr lnr-arrow-right"></span>']
      });



      /**********************\
      Post tabs
      /************************/
      $('.post-tab-list').on('click', 'a', function(e) {
          e.preventDefault();
        $('.post-tab-list a.active').removeClass('active');
          var tab = $(this).data('tab');
          $('.tab-content').removeClass('active');
          $('#' + tab).addClass('active');
          $(this).addClass('active');

      });


      /**********************\
      /// Scroll to top
      /************************/

      var doc_height = $(document).height();
      $(window).on('scroll', function() {
        if ($(this).scrollTop() > (doc_height - 1000)) {
            $('.scroll-top.active').removeClass('active');
            $('.scroll-top').addClass('active');
        } else {
            $('.scroll-top').removeClass('active');
        }
      });
      $('.scroll-top').on('click', function() {
          $("html, body").animate({
              scrollTop: 0
          }, 600);
          return false;
      });



      /**********************\
      // Fun fact count
      /************************/

      $('.fun-fact-count').countTo();  



      /**********************\
      // Equal height
      /************************/
      $(".equal-height").matchHeight();
    
    
    /// smooth scroll
    $('.page-scroll').on('click', function(event) {
        var $anchor = $(this);
        $('html, body').stop().animate({
            scrollTop: $($anchor.attr('href')).offset().top -70
        }, 1000, 'easeInOutExpo');
        event.preventDefault();
    });
    
    
    /**********************\
    //portfolio isotope init
    /************************/
    
    $('.portfolio-filter').on('click', 'li', function(){
      var filterValue = $(this).attr('data-filter');
      $('.portfolio-filter > li.active').removeClass('active');
      $(this).addClass('active')
      $('.portfolio').isotope({
        filter: filterValue
      });
    });
    
    var $portfolio = $('.portfolio'); 
    $portfolio.imagesLoaded( function(){
      $portfolio.isotope({
        itemSelector : '.portfolio-item',
        layoutMode: 'fitRows'
      });
    });

    
    
 // WOW init    
  if($(window).width() > 767){
    var wow = new WOW(
    {       
      mobile:       false,      
    }
  );
  wow.init();

  }
    
 

  });
  
}(jQuery));


 
















