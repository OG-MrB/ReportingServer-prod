/*
 * @requires jQuery($), jQuery UI & sortable/draggable UI modules
 */

var GadgetHolder = function(GadgetContainer){

    this.jQuery = $;
    this.pGadgetContainer = GadgetContainer;
    this.settings = {
        columns : '.column',
        widgetSelector: '.gadget',
        handleSelector: '.gadget-head',
        contentSelector: '.gadget-content',
        colorClasses : ['color-yellow', 'color-red', 'color-blue', 'color-white', 'color-orange', 'color-green']
        /*
        ,widgetDefault : {
            movable: true,
            removable: true,
            collapsible: true,
            editable: true
        }
        */
    };
    
    this.init = function () {
        //this.attachStylesheet('gadgets.js.css');
        this.addWidgetControls();
        //this.makeSortable();
    };
 
	
    this.getWidgetSettings = function (id) {
        var $ = this.jQuery,
            settings = this.settings;

    	var gadgetSettings = this.pGadgetContainer.getGadgetSettingsById(id);
    	return gadgetSettings;
        //return (id&&settings.widgetIndividual[id]) ? $.extend({},settings.widgetDefault,settings.widgetIndividual[id]) : settings.widgetDefault;
    };
    
    this.addWidgetControls = function () {
        var gadgetHolder = this,
            $ = this.jQuery,
            settings = this.settings;
            
        $(settings.widgetSelector, $(settings.columns)).each(function () {
            var thisWidgetSettings = gadgetHolder.getWidgetSettings(this.id);
            
            if (thisWidgetSettings.removable) {
                $('<a href="#" class="remove">CLOSE</a>').mousedown(function (e) {
                    e.stopPropagation();    
                }).click(function () {
                    if(confirm('This widget will be removed, ok?')) {
                        $(this).parents(settings.widgetSelector).animate({
                            opacity: 0    
                        },function () {
                            $(this).wrap('<div/>').parent().slideUp(function () {
                                $(this).remove();
                            });
                        });
                    }
                    return false;
                }).appendTo($(settings.handleSelector, this));
            }
            
            if (thisWidgetSettings.editable) {
       	
                $('<a href="#" class="edit">EDIT</a>').mousedown(function (e) {
                    e.stopPropagation();    
                }).toggle(function () {
                	//$(this).css({backgroundPosition: '-66px 0', width: '55px'})
                    $(this)
                        .parents(settings.widgetSelector)
                            .find('.edit-box').show().find('input').focus();
                    return false;
                },function () {
                    $(this).css({backgroundPosition: '', width: ''})
                        .parents(settings.widgetSelector)
                            .find('.edit-box').hide();
                    return false;
                }).appendTo($(settings.handleSelector,this));
                
                $('<div class="edit-box" style="display:none;"/>')
                    .append('<ul><li class="item"><label>Gadget title</label><input value="' + $('h3',this).text() + '"/></li>')
                    .append((function(){
                        var colorList = '<li class="item"><label>Gadget color</label><ul class="colors">';
                        $(gadgetHolder.settings.colorClasses).each(function () {
                            colorList += '<li class="' + this + '"/>';
                        });
                        return colorList + '</ul>';
                    })())
                    .append('</ul>')
                    .insertAfter($(settings.handleSelector,this));
            }
            /*
			var menuLink = '<div class="menu"><table border="0" cellpadding="0" cellspacing="0" class="container">';
			menuLink += '<tr>';
			menuLink += '<td class="myMenu">';
			menuLink += '<table class="rootVoices" cellspacing="0" cellpadding="0" border="0" width="20px">';
			menuLink += '<tr>';
			menuLink += '<td class="rootVoice ';
			menuLink += " {menu: 'menu_main'}";
			menuLink += '"><img src="../images/menu.png" style="width:20px; height:16px"></td>';				
			menuLink += '</tr>';
			menuLink += '</table>';
			menuLink += '</td>';
			menuLink += '</tr>';
			menuLink += '</table></div>';
            $(menuLink).appendTo($(settings.handleSelector,this));
			*/
                         
            $('<a href="#" class="refresh">Refresh</a>').mousedown(function (e) {
                e.stopPropagation();    
            }).click(function () {
	            var objName = window.gadgetContainer.getGadgetObjectNameById(thisWidgetSettings);
                //alert('Need to refresh content for: '+objName);
                eval(objName + '.refreshContent()');
                return false;

            }).appendTo($(settings.handleSelector,this));
                
            if (thisWidgetSettings.collapsible) {
                $('<a href="#" class="collapse">COLLAPSE</a>').mousedown(function (e) {
                    e.stopPropagation();    
                }).toggle(function () {
                    $(this).css({backgroundPosition: '-38px 0'})
                        .parents(settings.widgetSelector)
                            .find(settings.contentSelector).hide();
                    return false;
                },function () {
                    $(this).css({backgroundPosition: ''})
                        .parents(settings.widgetSelector)
                            .find(settings.contentSelector).show();
                    return false;
                }).prependTo($(settings.handleSelector,this));
            }
        });
        
        $('.edit-box').each(function () {
            $('input',this).keyup(function () {
                $(this).parents(settings.widgetSelector).find('h3').text( $(this).val().length>20 ? $(this).val().substr(0,20)+'...' : $(this).val() );
            });
            $('ul.colors li',this).click(function () {
                
                var colorStylePattern = /\bcolor-[\w]{1,}\b/,
                    thisWidgetColorClass = $(this).parents(settings.widgetSelector).attr('class').match(colorStylePattern)
                if (thisWidgetColorClass) {
                    $(this).parents(settings.widgetSelector)
                        .removeClass(thisWidgetColorClass[0])
                        .addClass($(this).attr('class').match(colorStylePattern)[0]);
                }
                return false;
                
            });
        });
        
    };
    
    this.attachStylesheet = function (href) {
        var $ = this.jQuery;
        return $('<link href="' + href + '" rel="stylesheet" type="text/css" />').appendTo('head');
    };
    
    
    this.makeSortable = function () {
        var gadgetHolder = this,
            $ = this.jQuery,
            settings = this.settings,
            /*
            $sortableItems = (function () {
                var notSortable = '';
                $(settings.widgetSelector,$(settings.columns)).each(function (i) {
                    if (!gadgetHolder.getWidgetSettings(this.id).movable) {
                        if(!this.id) {
                            this.id = 'gadget-no-id-' + i;
                        }
                        notSortable += '#' + this.id + ',';
                    }
                });
                return $('> li:not(' + notSortable + ')', settings.columns);
            })();
            */
            $sortableItems = (function () {
                items = $('> li', settings.columns);
                return items;
            })();
        
        $sortableItems.find(settings.handleSelector).css({
            cursor: 'move'
        }).mousedown(function (e) {
            $sortableItems.css({width:''});
            $(this).parent().css({
                width: $(this).parent().width() + 'px'
            });
        }).mouseup(function () {
            if(!$(this).parent().hasClass('dragging')) {
                $(this).parent().css({width:''});
            } else {
                $(settings.columns).sortable('disable');
            }
        });
        
        $(settings.columns).sortable({
        //$("#column1, #column2, #column3").sortable({
            items: $sortableItems,
            connectWith: ".column",
            handle: settings.handleSelector,
            placeholder: 'gadget-placeholder',
            forcePlaceholderSize: true,
            revert: 300,
            delay: 100,
            opacity: 0.8,
            containment: 'document',
            start: function (e,ui) {
                $(ui.helper).addClass('dragging');
            },
            stop: function (e,ui) {
                $(ui.item).css({width:''}).removeClass('dragging');
                $(settings.columns).sortable('enable');
                var item = $(ui.item);				
				
				var items = [];
				$(settings.columns).each(function () {
					idxval = $(this).attr("index");
					clidx = parseInt(idxval);
					items.push([]);
					$(this).children('li').each(function(idx, elm) {						
						items[clidx-1].push(elm.id)
					});
				});
				
				//check which column has no items
				//get its width and evenly distribute among others
				var widthForColWithNoItems;
				$(settings.columns).each(function () {
					idxval = $(this).attr("index");
					clidx = parseInt(idxval);
					if(items[clidx-1].length==0){
						widthForColWithNoItems = $(this).css('width');	
						$(this).css('width', "0%");
					}
				});
				
				var extraWidth = widthForColWithNoItems.replace("px","");
				var extraWidthPerCol = parseInt(extraWidth);
				if(items.length>1){
					extraWidthPerCol = extraWidthPerCol / (items.length - 1);
				}
				
				
				$(settings.columns).each(function () {
					idxval = $(this).attr("index");
					clidx = parseInt(idxval);
					if(items[clidx-1].length!=0){
						var tmpWidth = $(this).css('width');
						tmpWidthVal = tmpWidth.replace("px","");
						newWidthVal = parseInt(tmpWidthVal) + extraWidthPerCol;
						$(this).css('width', newWidthVal+"px");
					}
				});
				
            }
        });
/*
        $(settings.columns).sortable({
            items: $sortableItems,
            connectWith: $(settings.columns),
            handle: settings.handleSelector,
            placeholder: 'gadget-placeholder',
            forcePlaceholderSize: true,
            revert: 300,
            delay: 100,
            opacity: 0.8,
            containment: 'document',
            start: function (e,ui) {
                $(ui.helper).addClass('dragging');
            },
            stop: function (e,ui) {
                $(ui.item).css({width:''}).removeClass('dragging');
                $(settings.columns).sortable('enable');
                var items = $(settings.columns).sortable("option","items");
				alert(items);
            },
            update: function (e,ui) {
            }
        });
*/		
    };
    
};

