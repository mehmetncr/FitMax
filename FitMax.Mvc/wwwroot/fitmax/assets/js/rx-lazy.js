(function ( $ ) {
	$.fn.rxLazy = function( settings ) {

		if( ! this[0] ){
			return this;
		}

		var items = $( this ),
			mainClass = 'rx-lazy_item',
			hiddenClass = 'rx-lazy_hidden',
			imageBackGround = '#eff0f1';

		items
			.addClass( hiddenClass + ' ' + mainClass );

		if( ! window.rxLazy_windowLoad ){
			$( window ).on( 'load.rxLazy', init );
		}else{
			init();
		}

		function init(){
			$( window )
				.on( 'scroll.rxLazy', setItemVisibile )
				.trigger( 'scroll.rxLazy' );
		}

		function setItemVisibile(){
			items = items.map( function( index, element ) {
				var item = $( element );

				if( isInViewport(item) ){
					item
						.on( 'load', showAnimation )
						.attr( 'src', item.data('src') );
				}else{
					return element;
				}
			})
		}

		function isInViewport( htmlElement, indent = 500 ) {
			var htmlElement =  $( htmlElement );

			return ( window.pageYOffset <= htmlElement.offset().top && htmlElement.offset().top <= window.pageYOffset + window.innerHeight + indent );
		};

		function showAnimation(){
			var item = $( this );
			item.removeClass(hiddenClass);
		}
	};

	$( window ).on( 'load.rxLazy', function(){
		window.rxLazy_windowLoad = true;
	} );

}( jQuery ));
