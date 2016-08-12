/**
 * jQuery Extend
 */
$.extend($,{
	getDataIDEl:function(data_id){ return $('[data-id='+data_id+']'); },
	getDataWrapperEl:function(data_wrapper){ return $('[data-wrapper='+data_wrapper+']'); },
	getDataTargetEl:function(data_target){ return $('[data-target='+data_target+']'); },
	getDataContentEl:function(data_content){ return $('[data-content='+data_content+']'); }
});

$.fn.extend({
	findCustom:function(custom_id, value){ return $(this).find('['+custom_id+'='+value+']'); },
	findDataID:function(data_id){ return $(this).find('[data-id='+data_id+']'); },
	filterDataID:function(data_id){ return $(this).filter('[data-id='+data_id+']'); }
});

/**
 * Toggle Content
 */
var toggle = {
	clickEvt : function() {
		$.getDataTargetEl('btn-toggle').click(function() {
			var $this = $(this);
			var $wrapperToggle = $this.parents('[data-wrapper=wrapper-toggle]');
			var $toggleIcon = $this.find($.getDataIDEl('toggle-icon'));
			var $toggleContent = $wrapperToggle.find($.getDataContentEl('content-toggle'));
			var $toggleText = $this.find($.getDataIDEl('toggle-text'));
			if ( $wrapperToggle.attr('data-value') == 'unfold' ) {
				$wrapperToggle.attr('data-value', 'fold');
				$toggleIcon.removeClass('fa-angle-up').addClass('fa-angle-down');
				$toggleContent.addClass('hide');
				$toggleText.text("상세내역 보기");
			} else {
				$wrapperToggle.attr('data-value', 'unfold');
				$toggleIcon.removeClass('fa-angle-down').addClass('fa-angle-up');
				$toggleContent.removeClass('hide');
				$toggleText.text("상세내역 닫기");
			}
		});
	},
	init : function() {
		this.clickEvt();
	}
};

/**
 * #aside 영역 fixed로 처리되어있는 상태
 * 가로 스크롤 움직일 때 따라 움직이지 않도록 처리
 */
var aside = {
	selectedFind : function() {
		$('#aside').find('.selected').parents('li').addClass('unfold');
	},
	unfoldMenu : function() {
		$.getDataIDEl('unfold-menu').click(function() {
			$(this).parent().toggleClass('unfold');
		});
	},
	scrollPosition : function() {
		var scrollXPosition = $(window).scrollLeft();
		$('#aside').css('left', -scrollXPosition);
	},
	init : function() {
		this.unfoldMenu();
		this.selectedFind();
		this.scrollPosition();
	}
};

$(document).ready(function() {
	aside.init();
	toggle.init();
	$(window).scroll(function() {
		aside.scrollPosition();
	});
});