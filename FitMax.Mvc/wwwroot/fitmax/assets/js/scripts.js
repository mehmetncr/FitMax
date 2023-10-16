(function($){
	"use strict";

	$(window).on( 'load', function(){
		/*------------------- PRELOADER ------------------*/
		$('body').addClass('page-loaded');
	});

	/* ---------------------- MENU --------------------- */
	$( 'body' ).on('click', '.nav-btn',  function( event ){
		$( event.currentTarget ).toggleClass('active');
		$('.nav-menu').toggleClass('active');
		$( 'body' ).toggleClass('no-scroll');
		return false;
	});

	$( window ).on('resize.myTemplate', function(){
		$('body')[ ( $(this).width() <= 767 ) ? 'addClass' : 'removeClass' ]('isMobile')
	}).trigger( 'resize.myTemplate' );

	$( 'body' ).on('click', '.dropdown>a', function( event){
		if( ! $('body.isMobile')[0] ){
			return;
		}

		var $thisLi = $( event.currentTarget ).parents( 'li' ),
		$thisLiActive = $thisLi.hasClass( 'dropdown-active' );

		$('.dropdown-active').removeClass('dropdown-active').children('ul').slideUp('slow');

		if( ! $thisLiActive ){
			$thisLi.addClass('dropdown-active');
			$thisLi.children('ul').slideDown('slow');
		}

		return false;
	});

	$( 'body' ).on( 'mouseenter', '.dropdown', function(event){
		if( $('body.isMobile')[0] ){
			return;
		}

		var menuItem = $( event.currentTarget );

		if( menuItem[0].timeOutMenu ){
			clearTimeout( menuItem[0].timeOutMenu );
		}

		menuItem.addClass('active');
	}).on( 'mouseleave', '.dropdown', function( event ){
		if( $('body.isMobile')[0] ){
			return;
		}

		var menuItem = $( event.currentTarget );

		menuItem[0].timeOutMenu = setTimeout( function(){
			menuItem.removeClass('active');
		}, 500 );
	});

	/* --------------------- PARALLAX -------------------- */
	if( $( '.scene' )[0] ){
		$('.scene').each( function( index, element ){
			new Parallax( element );
		});
	}

	/* --------------------- TO TOP -------------------- */
	$(window).on( 'scroll.myTemplat', scrollWindow).trigger( 'scroll.myTemplat' );

	function scrollWindow() {
		if ($(window).scrollTop() > 500) {
			$('.to-top').addClass('active');
		} else {
			$('.to-top').removeClass('active');
		}
	}

	$( 'body' ).on( 'click', '.to-top',  function(event) {
		$('html, body').animate({
			scrollTop:0
		}, 400);
		return false;
	});

	/*---------------------- LAZY ---------------------*/
	if( $( '.rx-lazy' )[0] ){
		$('.rx-lazy').rxLazy();
	}

	/* ----------------- CONTACT FORM ----------------- */
	if( $( '#contactform' )[0] ){
		$( 'body' ).on( 'submit', '#contactform', function() {
			var action = $( this ).attr( 'action' ),
			message = $( '#message' ),
			submit = $( '#submit' );

			message.slideUp( 750, function() {
				message.hide();
				submit.attr( 'disabled', 'disabled' );
				$.post(
					action,
					{
						name: $( '#name' ).val(),
						email: $( '#email' ).val(),
						comments: $( '#comments' ).val(),
					},
					function( event ) {
						document.getElementById( 'message' ).innerHTML = event;
						message.slideDown( 'slow' );
						submit.removeAttr( 'disabled' );

						if ( null != event.match( 'success' ) ) {
							$( '#contactform' ).slideDown( 'slow' );
						}
					}
					);
			});
			return false;
		});
	}

	/* ---------------- CROSSFIT-SLIDER --------------- */
	if($('.crossfit-slider')[0]){
		$('.crossfit-slider').slick({
			infinite: true,
			slidesToShow: 1,
			slidesToScroll: 1,
			dots: true,
			arrows: true,
			speed: 800,
			fade: true,
			pauseOnHover: false,
			cssEase: 'ease-in-out',
			cssEase: 'cubic-bezier(0.7, 0, 0.3, 1)',
			touchThreshold: 100,
			nextArrow: '<span class="slick-arrow-next"><svg width="21" height="35" viewBox="0 0 21 35" fill="none"><path fill-rule="evenodd" clip-rule="evenodd" d="M19.321 15.7709C20.2315 16.6814 20.2316 18.1578 19.321 19.0683L4.48254 33.907C3.57198 34.8176 2.09566 34.8176 1.18508 33.907C0.27451 32.9964 0.2745 31.5201 1.18506 30.6095L14.3748 17.4196L1.18489 4.2299C0.274316 3.31934 0.274306 1.84302 1.18487 0.932447C2.09543 0.0218749 3.57175 0.0218652 4.48232 0.932426L19.321 15.7709Z" /></svg></span>',
			prevArrow: '<span class="slick-arrow-prev"><svg width="20" height="35" viewBox="0 0 20 35" fill="none"><path fill-rule="evenodd" clip-rule="evenodd" d="M0.682939 19.0686C-0.227635 18.158 -0.227644 16.6817 0.682916 15.7711L15.5214 0.932499C16.4319 0.0219266 17.9083 0.0219115 18.8188 0.932476C19.7294 1.84304 19.7294 3.31936 18.8188 4.22993L5.62911 17.4198L18.819 30.6096C19.7296 31.5201 19.7296 32.9965 18.819 33.907C17.9085 34.8176 16.4322 34.8176 15.5216 33.9071L0.682939 19.0686Z"/></svg></span>',
			appendArrows: $('.slider-navigation-cover'),
			appendDots: $('.slider-navigation-cover'),
			// autoplay: true,
			// autoplaySpeed: 4500,
			responsive: [
			{
				breakpoint: 768,
				settings: {
					dots: false,
					arrows: false,
				}
			}
			]
		});
	}

	/* ----------------- FITNESS-SLIDER ---------------- */
	if($('.fitness-slider')[0]){
		$('.fitness-slider').slick({
			infinite: true,
			slidesToShow: 1,
			slidesToScroll: 1,
			dots: true,
			arrows: true,
			speed: 800,
			fade: true,
			pauseOnHover: false,
			cssEase: 'ease-in-out',
			cssEase: 'cubic-bezier(0.7, 0, 0.3, 1)',
			touchThreshold: 100,
			nextArrow: '<span class="slick-arrow-next"><svg width="21" height="35" viewBox="0 0 21 35" fill="none"><path fill-rule="evenodd" clip-rule="evenodd" d="M19.321 15.7709C20.2315 16.6814 20.2316 18.1578 19.321 19.0683L4.48254 33.907C3.57198 34.8176 2.09566 34.8176 1.18508 33.907C0.27451 32.9964 0.2745 31.5201 1.18506 30.6095L14.3748 17.4196L1.18489 4.2299C0.274316 3.31934 0.274306 1.84302 1.18487 0.932447C2.09543 0.0218749 3.57175 0.0218652 4.48232 0.932426L19.321 15.7709Z" /></svg></span>',
			prevArrow: '<span class="slick-arrow-prev"><svg width="20" height="35" viewBox="0 0 20 35" fill="none"><path fill-rule="evenodd" clip-rule="evenodd" d="M0.682939 19.0686C-0.227635 18.158 -0.227644 16.6817 0.682916 15.7711L15.5214 0.932499C16.4319 0.0219266 17.9083 0.0219115 18.8188 0.932476C19.7294 1.84304 19.7294 3.31936 18.8188 4.22993L5.62911 17.4198L18.819 30.6096C19.7296 31.5201 19.7296 32.9965 18.819 33.907C17.9085 34.8176 16.4322 34.8176 15.5216 33.9071L0.682939 19.0686Z"/></svg></span>',
			appendArrows: $('.slider-navigation-cover'),
			appendDots: $('.slider-navigation-cover'),
			// autoplay: true,
			// autoplaySpeed: 4500,
			responsive: [
			{
				breakpoint: 768,
				settings: {
					dots: false,
					arrows: false,
				}
			}
			]
		});
	}

	/* -------------- TESTIMONIALS-SLIDER ------------- */
	if( $('.testimonials-slider')[0] ){
		$( '.testimonials-slider' ).slick({
			slidesToShow: 1,
			slidesToScroll: 1,
			arrows: false,
			dots: true,
			speed: 800,
			fade: true,
			touchThreshold: 200,
			cssEase: 'cubic-bezier(0.7, 0, 0.3, 1)',
			autoplay: true,
			autoplaySpeed: 4500,
		});
	}

	/* -------------- BEST-TRAINER-SLIDER ------------- */
	if( $('.best-trainer-slider')[0] ){
		$( '.best-trainer-slider' ).slick({
			slidesToShow: 5,
			slidesToScroll: 1,
			arrows: false,
			dots: false,
			speed: 800,
			touchThreshold: 200,
			cssEase: 'cubic-bezier(0.7, 0, 0.3, 1)',
			autoplay: true,
			autoplaySpeed: 4500,
			responsive: [
			{
				breakpoint: 992,
				settings: {
					slidesToShow: 4
				}
			},
			{
				breakpoint: 768,
				settings: {
					slidesToShow: 3
				}
			},
			{
				breakpoint: 481,
				settings: {
					slidesToShow: 2,
					slidesToScroll: 1,
				}
			}
			]
		});
	}

	/* -------------- SLIDER-CATEG-BICYKLE ------------- */
	if( $('.clients-cover')[0] ){
		$( '.clients-cover' ).slick({
			infinite: true,
			slidesToShow: 6,
			slidesToScroll: 1,
			arrows: false,
			speed: 800,
			touchThreshold: 200,
			cssEase: 'cubic-bezier(0.7, 0, 0.3, 1)',
			autoplay: true,
			autoplaySpeed: 4500,
			responsive: [
			{
				breakpoint: 576,
				settings: {
					slidesToShow: 3,
					slidesToScroll: 3,
				}
			}
			]
		});
	}

	/*---------------- COUNTER-ANIMATE ----------------*/
	if ( $('.counter-animate.counter-active')[0] ) {
		$( window ).on( 'scroll', function(){
			var winScrollTop = $( this ).scrollTop(),
			windowHeight = $(this).height();

			$('.counter-animate.counter-active').each(function(){
				var $this = $(this),
				targetPos = $this.offset().top;
				if( targetPos < winScrollTop + 100 + windowHeight / 2 ){
					if( $this.hasClass( 'counter-active' )){
						var time = 4;
						$('.about-info-col .number').each(function(){
							var i = 1,
							num = $(this).data('number'),
							step = 500 * time / num,
							that = $(this),
							int = setInterval(function(){
								if (i <= num) {
									that.html(i);
								}
								else {
									clearInterval(int);
								}
								i++;
							},step);
						});
						$this.removeClass('counter-active');
					}
				}
			});
		});
	}

	/*------------------ OPEN SIDEBAR ------------------*/
	$( 'body' ).on('click', '.btn-sidebar', function(){
		$('.sidebar .widgets').toggle('ease');
		return false;
	});

	/*-------------------- COUNTDOWN -------------------*/
	if( $( '#clockdiv' )[0] ){
		function getTimeRemaining(endtime) {
			var t = Date.parse(endtime) - Date.parse(new Date());
			var seconds = Math.floor((t / 1000) % 60);
			var minutes = Math.floor((t / 1000 / 60) % 60);
			var hours = Math.floor((t / (1000 * 60 * 60)) % 24);
			var days = Math.floor(t / (1000 * 60 * 60 * 24));
			return {
				'total': t,
				'days': days,
				'hours': hours,
				'minutes': minutes,
				'seconds': seconds
			};
		}

		function initializeClock(id, endtime) {
			var clock = document.getElementById(id);
			var daysSpan = clock.querySelector('.days');
			var hoursSpan = clock.querySelector('.hours');
			var minutesSpan = clock.querySelector('.minutes');
			var secondsSpan = clock.querySelector('.seconds');

			function updateClock() {
				var t = getTimeRemaining(endtime);

				daysSpan.innerHTML = t.days;
				hoursSpan.innerHTML = ('0' + t.hours).slice(-2);
				minutesSpan.innerHTML = ('0' + t.minutes).slice(-2);
				secondsSpan.innerHTML = ('0' + t.seconds).slice(-2);

				if (t.total <= 0) {
					clearInterval(timeinterval);
				}
			}

			updateClock();
			var timeinterval = setInterval(updateClock, 1000);
		}

		var deadline = new Date(Date.parse(new Date()) + 15 * 24 * 60 * 60 * 1000);
		initializeClock('clockdiv', deadline);
	}

	/* -------------------- ISOTOPE ------------------- */
	if( $( '.grid-gallery' )[0] ){
		var $grid = $('.grid-gallery').isotope({
			itemSelector: '.gallery-item',
			percentPosition: true,
			masonry: {
				columnWidth: '.grid-sizer'
			}
		});

		$( window ).on( 'load', rebuildMasonry ).on( 'resize', rebuildMasonry );
		function rebuildMasonry(){
			$grid.isotope();
		}
	}
	/* ------------------- FANCYBOX 3 ------------------ */
	if( $( '[data-fancybox]' )[0] ){
		$( '[data-fancybox]' ).fancybox({
			loop: true,
			infobar: false,
			transitionEffect: 'tube',
			buttons: [
			'close'
			],
		});
	}

	/*-------------------- GOOGLE MAP -------------------*/
	if( $( '.google-map' )[0] ){
		googleMapsInit();

		function googleMapsInit() {
			var mapOptions = {
				zoom: 11,
				center: new google.maps.LatLng(40.6501038, -73.9495823),
				mapTypeControl: false,
				fullscreenControl: false,
				scalecontrol: false,
				zoomControl: false,
				streetViewControl: false,
				rotateControl: false,
				styles: [{'featureType':'all','elementType':'labels.text.fill','stylers':[{'saturation':36},{'color':'#000000'},{'lightness':40}]},{'featureType':'all','elementType':'labels.text.stroke','stylers':[{'visibility':'on'},{'color':'#000000'},{'lightness':16}]},{'featureType':'all','elementType':'labels.icon','stylers':[{'visibility':'off'}]},{'featureType':'administrative','elementType':'geometry.fill','stylers':[{'color':'#000000'},{'lightness':20}]},{'featureType':'administrative','elementType':'geometry.stroke','stylers':[{'color':'#000000'},{'lightness':17},{'weight':1.2}]},{'featureType':'landscape','elementType':'geometry','stylers':[{'color':'#000000'},{'lightness':20}]},{'featureType':'poi','elementType':'geometry','stylers':[{'color':'#000000'},{'lightness':21}]},{'featureType':'road.highway','elementType':'geometry.fill','stylers':[{'color':'#000000'},{'lightness':17}]},{'featureType':'road.highway','elementType':'geometry.stroke','stylers':[{'color':'#000000'},{'lightness':29},{'weight':0.2}]},{'featureType':'road.arterial','elementType':'geometry','stylers':[{'color':'#000000'},{'lightness':18}]},{'featureType':'road.local','elementType':'geometry','stylers':[{'color':'#000000'},{'lightness':16}]},{'featureType':'transit','elementType':'geometry','stylers':[{'color':'#000000'},{'lightness':19}]},{'featureType':'water','elementType':'geometry','stylers':[{'color':'#000000'},{'lightness':17}]}]
			},
			mapElement = document.getElementById( 'map' ),
			map = new google.maps.Map( mapElement, mapOptions ),
			marker = new google.maps.Marker( {
				position: new google.maps.LatLng( 40.6401038, -73.9495823 ),
				map: map,
				icon: 'assets/img/map-point.png'
			});
		}
	}

}(jQuery));
