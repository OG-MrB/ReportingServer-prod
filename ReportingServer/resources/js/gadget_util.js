
/*
   Function: addContentScrolling
   A general function that enables scrolling for gadgets with overflow content.
   
   Parameters:
   
	contentHandle - The reference to the content to scroll
	
	maxrows - The line height limit for scrolling
	
*/
function addContentScrolling(contentHandle,maxrows) {	
	contentHandle.addClass('sec-scrl');
	contentHandle.css("height",(maxrows*20));
}

/*
   Function: addContentScrollingScale
   A general function that enables scrolling for gadgets with overflow content.
   
   Parameters:
   
	contentHandle - The reference to the content to scroll
	
	maxrows - The line height limit for scrolling
	
	scale - A multiplier for the height of the entity
	
*/
function addContentScrollingScale(contentHandle,maxrows, scale) {	
	contentHandle.addClass('sec-scrl');
	contentHandle.css("height",(maxrows*scale));
}

/*
   Function: setupDatatables
   A general function that initializes the settings for a gadget using datatables JQuery plugin
   
   Parameters:
   
	tableId - The id of the table specific to the gadget to be set up
	
*/
function setupDatatables(tableId) {
	$('#'+tableId+'_table').dataTable( {
		"bFilter":false,
		"bInfo": false,
		"oLanguage": { "sInfo": "",
					   "sInfoEmpty": ""},
		"aaSorting": [],
		"bAutoWidth" : true,
		"bPaginate": false,
		"bScrollCollapse": true
	} );
	
	//reset the min-height so that the gadget-content resizes properly when there is only 1 row of data
	$('#' + tableId + '_table_wrapper').css("min-height", "0px");
}

/*
   Function: setupDatatablesBoldFirstCol
   A custom function to bold the first column in a datatable
   
   Parameters:
   
	tableId - The id of the table specific to the gadget to be set up
	
*/
function setupDatatablesBoldFirstCol(tableId) {
		
    var firstCol = $('#'+tableId+'_table').dataTable({
            "bFilter":false,
			"bInfo": false,
			"oLanguage": { "sInfo": "",
							"sInfoEmpty": ""},
			"aaSorting": [],
			"bAutoWidth" : true,
			"bPaginate": false,
			"bScrollCollapse": true,
			"fnCreatedRow": function(nRow, aData, iDataIndex ) {

				$('td:eq(0)', nRow).css( 'font-weight', 'bold' );
			}
            
    });

	//reset the min-height so that the gadget-content resizes properly when there is only 1 row of data
    $('#'+tableId+'_table_wrapper').css("min-height", "0px");
  
}
/*
   Function: setupFixedDatatableColumn(tableId)
   A custom function to fix the first column in a datatable
   
   Parameters:
   
	tableId - The id of the table specific to the gadget to be set up
	
*/
function setupFixedDatatableColumn(tableId) {

    var oTable = $('#' + tableId + '_table').dataTable({
        "sScrollX": "100%",
        "sScrollInner": "150%",
        "bFilter": false,
        "bInfo": false,
        "oLanguage": {"sInfo": "",
                        "sInfoEmpty": ""},
        "aaSorting": [],
        "bAutoWidth": true,
        "bPaginate": false,
        "bScrollCollapse": true
    });

    new FixedColumns(oTable);

    //reset the min-height so that the gadget-content resizes properly when there is only 1 row of data
    $('#' + tableId + '_table_wrapper').css("min-height", "0px");

}
/*
   Function: XMLHttpRequestWrapper
   A wrapper for generic ajax calls (non ccl).  Uses JQuery $.ajax
   
   Parameters:
   
	Gadget - The gadget to receive the json repoly
	serviceUrl - The service URL
	funcCallback - The call back for this function which notifies the framework when complete
	async - A boolean which signifies asynchronous to the xmlhttprequest
*/	

function XMLHttpRequestWrapper(gadget, serviceUrl, funcCallback, async) {

	//allow cross site scripting
	jQuery.support.cors = true;
	
	// set the  Gadget text as Loading while the request processes
    $('#'+gadget.gadgetId).closest(".gadget_framework_loading_text").html("Loading...");
	
	var gadgetContentHandle = $('#'+gadget.gadgetId).children(".gadget-content");

	$.ajax({
		type: "GET",
		url: serviceUrl,
		data: "{}",
		contentType: "application/json; charset=utf-8",
		async: async,
		cache: false,
		dataType: "json",
		success: function (data, textStatus, jqXHR) {
			
			if (jqXHR.status == 200 && jqXHR.readyState == 4) {
				gadget.data = data;
				$('#'+gadget.gadgetId).children(".gadget_framework_loading_text").html("Rendering...");
				funcCallback(gadget);
				
			}	
		},
		error: function(XMLHttpRequest, textStatus, errorThrown) {
			if (isDebugEnabled) {
				logger.error("XMLHttpRequestWrapper failed: ", errorThrown);
			}	
			errMsg = '<strong>Error Retrieving Gadget Data! </strong><br /><ul><li>Error: ' + errorThrown + '</li></ul>';
			gadgetContentHandle.html(errMsg);
		}
  });
	
}	


/*
   Function: retrieveSettingsAndRenderLayout
    Pulls the settings for this app from the preference table.  Calls chb_mp_get_prefs
   
   Parameters:
	appId - the passed app id 
	
*/
function retrieveSettingsAndRenderLayout(gadgetPrefs){
	var paramsArray = [];
	var errMsg = "";
    
	var appName = "";
	var appColumnCnt = 1;
	var appHeaderConfig = "";
	var appHeaderURI = "";
	var appCustomCss = "";
	var appHorizCol = "";
	var arColumnWidths;
	var arRowHeights;
	
		//try{
			appName = gadgetPrefs.APP_NAME;
			appConfig = gadgetPrefs.APP_CONFIGS;
			$.each(appConfig, function(i, item) {
				
				if (item.CONFIG_NAME == "COLUMNS") {
					appColumnCnt = item.CONFIG_VALUE;
				}else if (item.CONFIG_NAME == "HEADER") {
					appHeaderConfig = item.CONFIG_VALUE;									
				} else if (item.CONFIG_NAME == "HEADER_URI") {
					appHeaderURI = item.CONFIG_VALUE;
				} else if (item.CONFIG_NAME == "CUSTOM_CSS") {
					appCustomCss = item.CONFIG_VALUE;
				} else if (item.CONFIG_NAME == "HORIZ_COL") {
					appHorizCol = item.CONFIG_VALUE;
				} else if (item.CONFIG_NAME == "COL_WIDTHS") {
					arColumnWidths = item.CONFIG_VALUE;
				} else if (item.CONFIG_NAME == "ROW_HEIGHTS") {
					arRowHeights = item.CONFIG_VALUE;
				}								
			});
				
			setupHeader(appHeaderConfig, appHeaderURI, appName, appCustomCss);
			setupGadgets(gadgetPrefs.GADGETS, appColumnCnt, appHorizCol, arColumnWidths, arRowHeights);
						
		//}catch (err) {//something failed in either the response or parsing
		//	errMsg = '<b>Javascript Error(retrieveSettingsAndRenderLayout)! </b><br /><ul><li>Message: ' + err.message + '</li><li>Name: ' + err.name + '</li><li>Number: ' + (err.number & 0xFFFF) + '</li><li>Description: ' + err.description + '</li></ul>';
		//	$("#gadgetContainer").html(errMsg);
		//}	
		/*	
		if (this.status != 200) {
			errMsg = '<b>Error Retreiving App Preferences! </b><br /><ul><li>Status: ' + this.status + '</li><li>Request: ' + this.requestText + '</li><li>Status Text: ' + this.statusText + '</li>';
			$("#gadgetContainer").html(errMsg);
		}
		*/
		
	setupFooter();
};

/*
Function: setupFooter
 Builds the HTML for the footer
*/
function setupFooter() {
    var ftr = "";
	$('body').append(ftr);
}

/*
   Function: setupHeader
    Builds the HTML for the header
   
   Parameters:
	appHeaderConfig - The type of header configuration to be used
	appHeaderURI - The location of the header file, if specified
	appName - app name
	appCustomCss - Attaches a custom CSS file
	
*/
function setupHeader(appHeaderConfig, appHeaderURI, appName, appCustomCss){
	if (appCustomCss.length > 0) {
		var scriptLoc = "/resources/" + appName + "/css/" + appCustomCss;
		loadExternalFile(scriptLoc, "css");
	}
	
	var hdr = "";
	var isLayoutDirty = false;
	var resetLayoutToDefault;
	//If this app has a custom header designated in the DB, go find the javascript file
	if (appHeaderConfig == "CUSTOM") {
		var scriptLoc = "../../resources/" + appName + "/js/" + appHeaderURI;		
		
		$.getScript(scriptLoc, function() {
			
		});
	
	} else if (appHeaderConfig == "NONE") {
		var hdr = "";
		if (isDebugEnabled) {
			hdr += '<input type="button" value="Hide console" onclick="toggleConsole(this)" />';
		}
		
		$('#consoleToggle').html(hdr);
		
	} else {
		var hdr = "";
		//setup app header
		/*
		hdr += '<div class="app-title-bar" style="padding-top:10px;padding-left:15px;">';
		hdr += '<h1>'+appName+'</h1>';
		//adding app content below the header title
		hdr += '<div class="app-header-content"><span style="font-weight:bold">This is the header content</span></div>';
		
		hdr += '<div id="headerlogin" class="loginHeader">';
		hdr += '<span id="user-name-login" style="margin:10px">Logged in as admin</span>';
		hdr += '<a href="/home" id="homeLink" role="button"><span class="ui-button-text">Home</span></a>';
		hdr += '<span id="manageUsersAnchor"><a href="admin/user" id="manageUsersLink" role="button"><span class="ui-button-text">Manage Users</span></a></span>';
		hdr += '<span id="manageRolesAnchor"><a href="admin/role" id="manageRolesLink" role="button"><span class="ui-button-text">Manage Roles</span></a></span>';
		hdr += '<a href="/logout" id="logout" role="button"><span class="ui-button-text">Sign Out</span></a>';
		hdr += '</div>';
		*/
		/*
		hdr += '<div style="color: white; background">';
		hdr += '<h2 class="header" style="margin-left:10px; margin-bottom:20px; margin-top:10px;">Monitoring Dashboard</h2>';
		hdr += '<div id="headerlogin" class="loginHeader">';
		hdr += '<span id="user-name-login" style="margin:10px">Logged in as admin</span>';
		hdr += '<a href="logout" id="logout" class="button">Manage Users</a>';
		hdr += '<a href="logout" id="logout" class="button">Manage Role</a>';
		hdr += '<a href="logout" id="logout" class="button">Sign Out</a>';
		hdr += '</div>';
		hdr += '<p class="copyrightLabel" style="position:relative; margin-left:-558px; margin-top:40px;">';
		hdr += '<b>2013 Birdi & Associates,Inc.</b>';
		hdr += '</p>';
		hdr += '</div>';
		*/
		$('body').prepend(hdr);
		
	}	
}

/*
   Function: setupGadgets
    Initializes the gadgetContainer and adds all gadgets to the page.
   
   Parameters:
	gadgets - An array of gadget objects
	columnCnt  - The number of columns in the app
	appHorizCol - If populated,  adjusts the widthPercent to compensate for a full spanning column
		
*/
function setupGadgets(gadgets, columnCnt, appHorizCol, arColumnWidths, arRowHeights){
	/*
		Variable: gadgetContainer
		Creates a new gadgetContainer for the app
    */
	window.gadgetContainer = new GadgetContainer();
	
	window.gadgetContainer.aColumnWidth = new Array(columnCnt);
	window.gadgetContainer.columnCnt = columnCnt;
	
	if(arRowHeights != undefined){
		window.gadgetContainer.aRowHeight = arRowHeights;
	}
	
	if(arColumnWidths != undefined){
		window.gadgetContainer.aColumnWidth = arColumnWidths;
	}else{
		
		if (appHorizCol != undefined && appHorizCol != "" && appHorizCol != null) {
			//columnCnt++;
			window.gadgetContainer.horizontalCol = appHorizCol;

			// calculate the width of each column with some padding of 1
			var widthPercent = (100 / (columnCnt - 1)) - 1;
			
		
			for (cw = 0; cw < columnCnt; cw++) {
				if (cw == parseInt(appHorizCol)) {
					window.gadgetContainer.aColumnWidth[cw] = "98%";
				} else {
					window.gadgetContainer.aColumnWidth[cw] = widthPercent + "%";
				}
			}
			
		} else {
		
			// calculate the width of each column with some padding of 1
			var widthPercent = (100 / columnCnt) - 1;
			
			// assign the percentages for each column
			for (var x = 0; x < columnCnt; x++) {
				window.gadgetContainer.aColumnWidth[x] = widthPercent + "%";
			}
		}
	}
	
	
	// set the initial color scheme - not used in this version
	//window.gadgetContainer.colorScheme = "gray";
	
	// set the css div id which will serve as the framework target
	window.gadgetContainer.frameworkDivId = "gadgetContainer";
	
	//loop through all available gadgets
	$.each(gadgets, function(i, gd) {	
		var configCount = gd.CONFIGS.length;
		var menuItems = "";
		var settingsString = {};
		//var serverEnv = '/resources/';
		//var serverEnv = 'http://64pvtw7n00010/examples/widget_framework/gadgets/';
		var serverEnv = 'resources/js/gadgets/';
		//var serverEnv = '';
		var menuCount = 0;
		
		settingsString['headerTitle'] = gd.GADGET_NAME;
		settingsString['gadgetType'] = gd.TYPE;
		settingsString['gadgetStyleClassName'] = gd.STYLE_CLASS_NAME;
		settingsString['gadgetRendererLocation'] = serverEnv + gd.VIEW_RENDERER_URI;
		settingsString['gadgetClassName'] = gd.CLASS_NAME;
		settingsString['gadgetConfigParameters'] = {scriptName: gd.SERVICE_URI};
		settingsString['gadgetId'] = gd.GADGET_ID;
		settingsString['columnNumber'] = gd.COL_VAL;
		if(gd.GADGET_WIDTH){
			settingsString['gadgetWidth'] = gd.GADGET_WIDTH;
		}
		settingsString['gadgetRowNumber'] = gd.ROW_VAL;
		settingsString['displayGadgetHeader'] = gd.DISPLAY_HEADER;
				
		if (configCount > 0) {
			for (var j = 0; j < configCount; j++) {
				var configType = gd.CONFIGS[j].CONFIG_TYPE;
				var configName = gd.CONFIGS[j].CONFIG_NAME;
				var configDisp = gd.CONFIGS[j].CONFIG_DISP;
				var configValue = gd.CONFIGS[j].CONFIG_VALUE;
				
				//added this condition to prevent any menu items that may not have a config_value to be added to
				//settingsString object
				if(configType == "Menu"){
					settingsString["menu_" + menuCount + "_type"] = configType;
					settingsString["menu_" + menuCount + "_name"] = configName;
					settingsString["menu_" + menuCount + "_disp"] = configDisp;
					settingsString["menu_" + menuCount + "_value"] = configValue;
					
					menuItems += configDisp + ",";
					menuCount++;
					
				} else if(configValue != undefined){
					settingsString[configName.toLowerCase()] = configValue;
				}			
			}
		}
		
		//remove the trailing ',' before adding it to settingsString object
		//settingsString['menuOptions'] = menuItems.substring(0, menuItems.length - 1);		
		window.gadgetContainer.setGadgetSetting(settingsString);	
	});

	window.gadgetContainer.renderGadgetColumns();

	window.gadgetContainer.decorateGadgets();

	window.gadgetContainer.loadGadgets();
}


/*
   Function: getZebraClass
    A simple function to assign zebra striping when not using datatables
   
   Parameters:
  
	index - A passed index which is most likely going to be a for loop index

   Returns:
	zebraClass - The css class for this row 

*/
function getZebraClass(index) {
	if (index%2 == 0) {
		zebraClass = "tableEven";
	}	
	else {
		zebraClass = "tableOdd";
	}	
	return zebraClass;
}

/*
   Function: buildGadgetContentECControl
    This function will wrap a completed HTML string containing a section of gadget content that needs
     to be wrapped in a expand / collapsed control.  This needs to be called in the gadget prior to writing the 
     HTML to the DOM and it must be followed by enableGadgetContentECControl.
   
   Parameters:
  
	passedHTML - A HTML to be wrapped in the control
	section Title - The title of the collapsable region
	uniqueId - A unique row id for the section
	expCol - Flag to add expand/collapse for each list
	expandAll - Flag to expand all options in the list

   Requires:
	enableGadgetContentECControl to be called after DOM is updated

*/
function buildGadgetContentECControl(passedHTML, sectionTitle, uniqueId, expCol, expandAll) {

	var finalHTML = "";
	if (expandAll == 1) {
		if (expCol == 0) {
		
			headerHTML = '<p class="heading expandAll" id=\"'+uniqueId+'\" onclick="setupExpandAll(this)">&nbsp;<strong>' + sectionTitle + '</strong></p>';
			headerHTML += '<div class="content sec-content-collapsed">';
	
		} else if (expCol == 1 ) {

			headerHTML = '<p class="heading expandAll" id=\"'+uniqueId+'\" onclick="setupExpandAll(this)">&nbsp;<strong>' + sectionTitle + '</strong></p>';
			headerHTML += '<div class="content sec-content-expanded">';
		
		}   else {

			headerHTML = '<p class="heading collapseAll" id=\"'+uniqueId+'\">&nbsp;<strong>' + sectionTitle + '</strong></p>';
			headerHTML += '<div class="content sec-content-expanded">';
		
		}
	} else {
		if (expCol == 0) { //collapsed
			
			headerHTML = '<p class="heading collapsed" id=\"'+uniqueId+'\" onclick="enableGadgetContentECControl(this)">&nbsp;<strong>' + sectionTitle + '</strong></p>';
			headerHTML += '<div class="content sec-content-collapsed">';
		
			
		} else { //expanded
			headerHTML = '<p class="heading expanded" id=\"'+uniqueId+'\" onclick="enableGadgetContentECControl(this)">&nbsp;<strong>' + sectionTitle + '</strong></p>';
			headerHTML += '<div class="content sec-content-expanded">';
		
		}
	}
	
	finalHTML = headerHTML + passedHTML + "</div>";

	return finalHTML;
}

/*
   Function: enableGadgetContentECControl
    This function will initialize the expand/collapsed region generated by buildGadgetContentECControl.
     Note, this must be called after the DOM is updated with the content.
   
   Parameters:
  
	header - Section header handle
   
   Requires:
	buildGadgetContentECControl to be called prior to build the content

*/
function enableGadgetContentECControl(header) {	
	$(header).toggleClass("expanded");
	$(header).toggleClass("collapsed");

	if ($(header).next(".content").hasClass("sec-content-collapsed")) {
		$(header).next(".content").removeClass("sec-content-collapsed");
		$(header).next(".content").addClass("sec-content-expanded");	
	} else if ($(header).next(".content").hasClass("sec-content-expanded")) {
		$(header).next(".content").removeClass("sec-content-expanded");
		$(header).next(".content").addClass("sec-content-collapsed");
	}
}

/*
   Function: setupExpandAll
    A UI function that sets up a controller header for multiple children expand / collapse sections
   
   Parameters:
  
	header - The controlling header element for all children

*/
function setupExpandAll(header){
	
	var contentHandle = $(header).next(".content");
	var contentChildHeading = $(contentHandle).children(".heading");

	$(header).toggleClass("expandAll");
	$(header).toggleClass("collapseAll");
	if ($(header).next(".content").hasClass("sec-content-collapsed")) {
		$(header).next(".content").toggleClass("sec-content-collapsed").toggleClass("sec-content-expanded").slideToggle(500);
	}	
	contentChildHeading.toggleClass("expanded");
	contentChildHeading.toggleClass("collapsed");

	
	if (contentChildHeading.next(".content").hasClass("sec-content-collapsed")) {
		contentChildHeading.next(".content").removeClass("sec-content-collapsed");
		contentChildHeading.next(".content").addClass("sec-content-expanded");	
	} else if (contentChildHeading.next(".content").hasClass("sec-content-expanded")) {
		contentChildHeading.next(".content").removeClass("sec-content-expanded");
		contentChildHeading.next(".content").addClass("sec-content-collapsed");
	}

}




/*
   Function: parseMenuItems
    A helper function that parses the menu items
   
   Parameters:
  
	menuOptions - the menu options for that gadget
	gadgetId - the id of the gadget

*/
function parseMenuItems(menuOptions, gadgetId) {
	var menuOptionArray = menuOptions.split(',');
	var menu = "";
	
	for (var i = 0; i < menuOptionArray.length; i++) {
		var menuItem = menuOptionArray[i];
		var menuItemParams = "";
		
		if (menuItem != "" && menuItem.length > 0) {
			var menuItemValue = gadgetContainer.getGadgetSettingByName(gadgetId, "MENU_" + i + "_VALUE");
			var menuItemType = gadgetContainer.getGadgetSettingByName(gadgetId, "MENU_" + i + "_TYPE");
			var menuItemName = gadgetContainer.getGadgetSettingByName(gadgetId, "MENU_" + i + "_NAME");
			var menuItemDisp = gadgetContainer.getGadgetSettingByName(gadgetId, "MENU_" + i + "_DISP");
			
			if (menuItemName == "POWERFORM") {
				menu += '<li><a href=\'javascript:APP_EVENT("POWERFORM",\"'+menuItemValue+'\")\' id="'+gadgetId+'-'+i+'" class="powerformMenuOpt"><span class="editSettingsMenu">'+menuItem+'</span></a></li>';
			} else if (menuItemName == "FLOWSHEET" || menuItemName == "ORDERPROFILE" || menuItemName == "FORMBROWSER" || menuItemName == "LAB") {
				menuItemParams = menuItemValue.split('|');
				menu += '<li><a href=\'javascript:APPLINK(0,"$APP_APPNAME$","/PERSONID=' + menuItemParams[0] + ' /ENCNTRID=' + menuItemParams[1] +  ' /FIRSTTAB=^' + menuItemParams[2] + '^")\' id="'+gadgetId+'-'+i+'" class="gotoPageMenuOpt"><span class="editSettingsMenu">'+menuItem+'</span></a></li>';
			} else if (menuItemName == "CCLLINK") {
				menuItemParams = menuItemValue.split('|');
				var linkVal = "'" + menuItemParams[0] + "', '" + menuItemParams[1] + "', " + menuItemParams[2];
				if (isDebugEnabled) {
					logger.debug("linkVal = ", linkVal);
				}

				menu += '<li><a href="javascript:CCLLINK(' + linkVal + ')" id="'+gadgetId+'-'+i+'" class="gotoPageMenuOpt"><span class="editSettingsMenu">'+menuItem+'</span></a></li>';
			} else if(menuItemName == "HELP") {
				menu += '<li><a href="javascript:alert(\'No help currently available\')" id="'+gadgetId+'-'+i+'" class="helpMenuOpt"><span class="editSettingsMenu">'+menuItem+'</span></a></li>';
			} else if(menuItemName == "REFRESH") {
				menu += '<li><a href="javascript:alert(\'Not currently implemented\')" id="'+gadgetId+'-'+i+'" class="refreshMenuOpt"><span class="editSettingsMenu">'+menuItem+'</span></a></li>';
			}
		}
	}
	return menu;
}


/*
   Function: generateToolTip
    Wraps content inside an <a href> tag which calls a tooltip on mouseover
   
   Parameters:
  
	content - The content of the tooltip
	title - The tooltip title
	width - The width of the tooltip
	
   Returns:
	tipHTML - The completely wrapped content

*/
function generateToolTip(content, title, width) {
	var tipHTML = '<a class="tooltip-trigger" href="#nolink" onMouseOver="Tip('+ "\'" + content + "\'" +',TITLE, '+"\'"+title+"\'"+', WIDTH, ' + width + ',CLICKSTICKY,true,CLICKCLOSE,true,OFFSETY,-200)" onMouseOut="UnTip();"></a>';
	
	return tipHTML;
}


/*
   Function: appendTextToHeader
    Adds any text to the gadget header
   
   Parameters:
  
	gadgetId - The gadgets id
	textToBeAppended - The text that needs to be appended to the header
*/
function appendTextToHeader(gadgetId, textToBeAppended){
	var gadgetHeadTitleHandle = $('#'+gadgetId).children(".gadget-head").children("h3");
	$(textToBeAppended).appendTo(gadgetHeadTitleHandle);
}

/*
   Function: buildDataTableHeader
    Builds the table header for a datatable 
   
   Parameters:
  
	gadgetId - The gadgets id
	hoverEnabled - boolean for enabling / disabling a hover in the header
	hoverText - Text to display on header hover icon
	hoverTitleText - The Title of the hover to display 
	hoverHeight - The  pixel height of the hover tooltip
	hoverWidth - The pixel width of the hover tooltip
	labelArray - The text labels to display in each column of the datatable
	colWidth - An optional array of column widths for the table, if null then defaults are used (content driven)
	
*/
function buildDataTableHeader(gadgetId, hoverEnabled, hoverText, hoverTitleText, hoverHeight, hoverWidth, labelArray, colWidth) {
	
	var numLabels = labelArray.length;
	
	jsHeaderHTML = '<table class="table_display" id="'+gadgetId+'_table">';
	jsHeaderHTML +=	'<thead>';
	jsHeaderHTML += '<tr align="left">';
	if (hoverEnabled == true) {
		jsHeaderHTML += '<th width="3.0%">';
		jsHeaderHTML += '<a class="tooltip-trigger" href="#nolink" onMouseOver="Tip('+ "\'" + hoverText + "\'" +',TITLE, '+"\'"+hoverTitleText+"\'"+',HEIGHT,'+ hoverHeight +', WIDTH,'+ hoverWidth +',CLICKSTICKY,true,CLICKCLOSE,true)" onMouseOut="UnTip();"></a>';
		jsHeaderHTML +=	'</th>';
	}
	for (var x = 0; x < numLabels; x++) {
		if (colWidth == undefined || colWidth == null) {
			jsHeaderHTML += '<th>&nbsp;<a href="#nolink">' + labelArray[x] + '</a>&nbsp;</th>';
		} else {
			jsHeaderHTML += '<th width="' + colWidth[x] + '%">&nbsp;<a href="#nolink">' + labelArray[x] + '</a>&nbsp;</th>';
		}
	}	
	jsHeaderHTML += '</tr>';
	jsHeaderHTML += '</thead>';
	jsHeaderHTML += '<tbody>';
	return jsHeaderHTML;

}

/*
   Function: loadExternalFile
    A helper function for loading external files (javascript, css)
   
   Parameters:
	filename - the actual name of the js or css you want to load
	filetype  - flag specifying which type of element to create (js or css)
  */ 
function loadExternalFile (filename, filetype) {

	if (filetype=="js") { //if filename is a external JavaScript file
		var fileref=document.createElement('script');
		fileref.setAttribute("type","text/javascript");
		fileref.setAttribute("src", filename);
	}
	else if (filetype=="css") { //if filename is an external CSS file
		var fileref=document.createElement("link");
		fileref.setAttribute("rel", "stylesheet");
		fileref.setAttribute("type", "text/css");
		fileref.setAttribute("href", filename);
	}
	if (typeof fileref!="undefined") {
		document.getElementsByTagName("head")[0].appendChild(fileref);
	}	
}

/*
   Function: subCleanHover
    Clean up any quotes that may break hovers...this probably will not be needed. 
   
   Parameters:
	String - The string to be cleaned
		
   Returns:
	The cleaned string
	
  */ 
function subCleanHover (string) {
	var cleanString = string;

	cleanString = cleanString.replace('"', "&quot;");

	return cleanString;
}

/*
   Function: showNotificationMessage
    Displays a dialogue div with a message to the user which times out
   
   Parameters:
	notificationMessage - The message to be displayed
	
  */ 
function showNotificationMessage(notificationMessage){	
	var dialogHTML = '<div id="save-dialog-content" style="font-weight:bold; font-size:12px; margin-top:25px; margin-bottom:25px; margin-left:45px;">'+notificationMessage+'</div>';
		dialogHTML += '	<div id="save-dialog-ok" align="center" style="width:100%">';
		dialogHTML += '		<a href="javascript:dismissNotificationMessage()"><img id="save-dialog" src="http://' + serverName + '.tch.harvard.edu/standards/images/ok.gif" /></a></div>';
		
	notificationDialog = $('<div>saveLayoutDialog</div>')
			.html(dialogHTML)
			.dialog({
				autoOpen: false,
				title: 'Saving Layout',
				width:250,
				height:120,
				resizable:false,
				closeOnEscape:true,
				modal:false
		});
	
	
	notificationDialog.dialog('open');
	
	setTimeout(function(){
		dismissNotificationMessage();
	}, 3000);	
}

/*
   Function: dismissNotificationMessage
    Forces a notificationDialogue to close

 */ 
function dismissNotificationMessage(){
	notificationDialog.dialog('close');
}

/*
   Function: getToday
    Returns today's date / time formatted as  MM/DD/YYYY HH:MI

 */ 
function getToday() {

	var d = new Date();
	var day = d.getDate();
	var month = (d.getMonth() + 1);
	var year = d.getYear();
	var hour = d.getHours();
	var minutes = d.getMinutes();
	if (minutes < 10) {
		minutes = "0" + minutes;
	}
	if (hour < 10) {
		hour = "0" + hour;
	}
	var dateString = month + "/" + day + "/" + year + " " + hour + ":" + minutes;
	return dateString;
}
