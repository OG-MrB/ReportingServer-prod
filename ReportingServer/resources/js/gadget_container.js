/*
   Class: GadgetContainer
   A container object that will contain all of the gadgets.
*/
var GadgetContainer = function(){

	/*
		Variable: arGadgetSettings
		An array of gadget settings
	 */		
	var arGadgetSettings = new Array();
	
	/*
		Variable: arDisplayColumns
		An array ofthe columns in the container view
	 */
	var arDisplayColumns = new Array();		
	
	/*
		Variable: defaultGadgetSettings
		The default gadget settings object
	 */
	var defaultGadgetSettings = {
		/*
			Variable: collapsed
			A boolean controlling the initial collapse state of the gadget
			
			Default: false
		*/
		collapsed : false,
		
		/*
			Variable: movable
			A boolean controlling the movable state of the gadget (JQuery)
			
			Default: true
		*/
		movable: true,
		
		/*
			Variable: removable
			A boolean controlling the removable state of the gadget 
			
			Default: false
		*/
        removable: false,
		
		/*
			Variable: collapsible
			A boolean controlling the collapsible state of the gadget 
			
			Default: true
		*/
        collapsible: false,
		
		/*
			Variable: editable
			A boolean controlling the editable state of the gadget (not implemented)
			
			Default: false
		*/
        editable: true,
		
		/*
			Variable: headerTitle
			The default string containing the title of the gadget-header div
			
			Default: "Mpages Gadget"
		*/
		headerTitle : "MPages Gadget",
		
		/*
			Variable: headerToolbarText
			The default string containing the title of the Header Toolbar
		*/
		headerToolbarText : "",
		
		/*
			Variable: gadgetStyleClassName
			The default css style attached to the gadget (NOT SURE IF IMPLEMENTED)
		*/
		gadgetStyleClassName : "",
		
		/*
			Variable: gadgetType
			The default gadget type (can be object, script, html)
			
			Default: "script"
		*/
		gadgetType : "script",   
		
		/*
			Variable: gadgetRendererLocation
			The default renderer location 
		*/
		gadgetRendererLocation : "",
		
		/*
			Variable: gadgetClassName
			The default gadget class name 
		*/
		gadgetClassName : "",
		
		/*
			Variable: gadgetConfigParameters
			The default gadget configuration parameters

		*/
		gadgetConfigParameters : {},
		
		/*
			Variable: expCol
			The state of how the gadget is going to be displayed
			
			Default: Collapsed
		*/		
		expCol : "Collapsed",
		
		/*
			Variable: columnNumber
			The default column number for a gadget
			
			Default: 1
		*/
		columnNumber :  1,
		
		/*
			Variable: gadgetId
			Private id for the gadget
		*/
		gadgetId : "",
		
		/*
			Variable: containerId
			Private containerId for the gadget
		*/
		containerId : "",
		
		/*
			Variable: object
			Private object for the gadget
		*/
		object : undefined,
		
		/*
			Variable: horizontalCol
			
		*/
		horizontalCol : undefined,
		
		/*
			Variable: objectName
			Private objectName for the gadget
		*/
		objectName : ""
	};
	
	/*
	Function: getGadgetSettings

	Getter method for gadget settings, attaches Jquery selector to $

	Parameters:

		id - The gadget id

	Returns:

		String 'Gadget Display Content for '+ gadgetId
	*/	
	this.getGadgetSettings = function (id) {
		var $ = this.jQuery,
		settings = this.settings;
		return 'Gadget Display Content for '+id;
	};
	
	/*
	Function: setGadgetSetting

	Setter method for gadget settings

	Parameters:

		settings - The settings for the gadget(s)

	*/	
	this.setGadgetSetting = function(settings) {
		

		//merge the gadget settings and the defaults 
		var combinedGadgetsettings = new Object();
		
		//use Jquery extend to merge all settings recursively
		//$.extend(true, combinedGadgetsettings, settings);
		$.extend(true, combinedGadgetsettings, defaultGadgetSettings, settings);
		
		//add gadget to array
		arGadgetSettings.push(combinedGadgetsettings);		
	};

	/*
	Function: getGadgetSettingByName

	Getter method for a specific gadget setting

	Parameters:

		id - The gadget id
		settingName - The setting/property to look up

	Returns:

		the value in the gadgetSettings array
	*/
    this.getGadgetSettingByName = function (id, settingName) {
    	var gadgetSettings = this.getGadgetSettingsById(id);
		settingName = settingName.toLowerCase();
		for (var setting in gadgetSettings) {
			if (setting == settingName) {
				return gadgetSettings[settingName];
			}
		}
    };

    this.setGadgetSettingByName = function (id, settingName, settingValue) {
    	var gadgetSettings = this.getGadgetSettingsById(id);
		settingName = settingName.toLowerCase();
		for (var setting in gadgetSettings) {	
			if (setting == settingName) {
				jQuery.logInfo("match found for "+setting);
				gadgetSettings[setting] = settingValue;
				break;
			}
		}
    };
    
	/*
		Variable: columnCnt
		The column count for this container
	 */
	this.columnCnt = 1;
	
	/*
		Variable: aColumnWidth
		An array of column widths for this container
	 */
	this.aColumnWidth = new Array();
	
	/*
		Variable: aRowHeight
		An array of row heights for this container
	 */
	this.aRowHeight = new Array();
	
	/*
		Variable: frameworkDivId
		The CSS div id to reference
	 */
	this.frameworkDivId = "mpages_gadget_framework";
	
	/*
		Variable: frameworkStyle
		The CSS style id to reference
	 */
	this.frameworkStyle = "column";
	
	/*
	Function: renderGadgetColumns

	This function  renders the HTML of the column divs by looping through the columns
	and writing the UL lists.
	
	Calls: createDisplayColumnArray()

	*/	
	this.renderGadgetColumns = function(){

		//create the column arrays from the gadget list
		createDisplayColumnArray(this);	
		
		//array to hold column width
		var aWidth = getColumnWidth(this);	
		
		//outer div
		var sHTML = '<div id="columns">';
						
		//loop through columns
		for (var i = 0; i < this.columnCnt; i++) {	

			//add column
			var colIndex = i + 1;
			var marginLeft = "0px";
			if(colIndex > 1){
				marginLeft = "-15px";
			}
			sHTML += '<ul id="column'+colIndex+'" index="'+colIndex+'" class="column" style="vertical-align:top;width:' + aWidth[i] + ';padding-left:5px;margin-left:'+marginLeft+';">';	
			
			var totalRows = this.aRowHeight[i].length;
			//loop through each columns gadgets
			for(var rowIndex = 1; rowIndex <= totalRows; rowIndex++){ 
				sHTML += '<div id="gdr_'+colIndex+'_'+rowIndex+'" class="gadgetRow">'; 
				for (var j = 0; j < arDisplayColumns[i].length; j++ ) {
					//add gadget 
					var iGadget = arDisplayColumns[i][j];
					var oGadget = this.getGadgetSettingsByIndex(iGadget);
					if(rowIndex == oGadget.gadgetRowNumber){
						sHTML += this.getGadgetHtml(iGadget, this, this.aRowHeight[i][rowIndex-1]);
					}
				}
				sHTML += '</div>';				
			}
			//end list
			sHTML += '</ul>';	
		}	
		
		
		//end div	
		sHTML += '</div>';			
		
		//draw on page
		$("#" + this.frameworkDivId).html(sHTML);
		
	};


	/*
	Function: createDisplayColumnArray

	This function  is called by renderGadgetColumns, it loops through the columns and assigns each gadget
	to a column id.

	Parameters:

		me - (This) GadgetContainer object

	*/
	var createDisplayColumnArray = function(me) {
	
		//create a sub-array for each column
		for (var i = 0; i < me.columnCnt; i++) {
			arDisplayColumns[i] = new Array();
		}
		
		//loop throught the gadgets and assign them to each column
		for (var iGadget = 0; iGadget < arGadgetSettings.length; iGadget++) {
		
			var iColumn = arGadgetSettings[iGadget].columnNumber;
			
			if (iColumn <= me.columnCnt) {
				//next index
				var iCount = arDisplayColumns[iColumn-1].length;
				
				//reference to Gadget by index
				arDisplayColumns[iColumn-1][iCount] = iGadget;
			}
		}
	};
	
	/*
	Function: getMaxColumnLength

	This function find the highest column length to set as a maximum

	Parameters:

		me - (This) GadgetContainer object

	*/
	var getMaxColumnLength = function(me) {
	
		// initialize max variable
		var iMax = 0;
		
		//loop through columns and find max
		for (var i = 0; i < arDisplayColumns.length; i++) {
		
			if (arDisplayColumns[i].length > iMax){
			
				iMax = arDisplayColumns[i].length;
				
			}
		}
	};
	
	/*
	Function: getColumnWidth

	This function find the highest column width to set as a maximum

	Parameters:

		me - (This) GadgetContainer object
	
	Returns: aWidth - Column width

	*/
	var getColumnWidth = function(me) {

		//check if aColumnWidth defined appropriately
		if (me.aColumnWidth.length == me.columnCnt) {
			return(me.aColumnWidth);	
		} else {
			//split evenly as percentage
			var sWidth = Math.round(100/me.columnCnt) + "%";
			
			var aWidth = new Array(me.columnCnt);
			
			for (var i = 0; i < aWidth.length; i++) {
			
				aWidth[i] = sWidth;
			}
			
			return(aWidth);
		}
	};
	
	/*
	Function: getGadgetSettingsByIndex

	This function gets the gadget settings via a passed index

	Parameters:

		iGadget - The gadget index
	
	Returns: arGadgetSettings[iGadget]

	*/	
	this.getGadgetSettingsByIndex = function(iGadget) {
		return arGadgetSettings[iGadget];
	};
	
	/*
	Function: getGadgetSettingsById

	This function gets the gadget settings via a passed id

	Parameters:

		iGadget - The gadget id
	
	Returns: arGadgetSettings[iGadget]

	*/
	this.getGadgetSettingsById = function(gadgetId) {
		for (var iGadget = 0; iGadget < arGadgetSettings.length; iGadget++) {
			if(gadgetId == arGadgetSettings[iGadget].gadgetId) {
				return arGadgetSettings[iGadget];
			}
		}		
	};
	
	this.getGadgetObjectNameById = function(gadgetSettings) {
		return gadgetSettings.gadgetClassName + "_obj";
	};
	
	/*
	Function: getAllGadgetSettings

	This function returns the  arGadgetSettings object
	
	Returns: arGadgetSettings

	*/
	this.getAllGadgetSettings = function() {
		return arGadgetSettings;
	};
};

/*
	Function: getGadgetHtml

	This  prototype function builds and returns the HTML for the gadget's body.

	Parameters:

		iGadget - The gadget id
		
		me -  the passed gadget container
	
	Returns: sHTML - the HTML for the gadget

	*/
GadgetContainer.prototype.getGadgetHtml = function(iGadget, me, rowHeight) {
	
	
	//reference to Gadget configuration object
	var oGadget = this.getGadgetSettingsByIndex(iGadget);

	//initialize html variable
	var sHTML = "";

	//set this gadget id
	oGadget.gadgetId = "gd-" + iGadget;
	
	var headerDisplay = "block";
	if(!oGadget.displayGadgetHeader){
		headerDisplay = "none";
	}
	
	//build the HTML for the gadget body HTML
	sHTML += '<div class="gadget '+oGadget.gadgetStyleClassName+'" id="'+oGadget.gadgetId+'" style="width:'+oGadget.gadgetWidth+';float:left;">'  
    	+ '<div class="gadget-head" style="display:'+headerDisplay+'">'
    	+ '<h3>'+oGadget.headerTitle+'</h3>'
    	+ '</div>'
    	+ '<div class="gadget-content" style="height:'+rowHeight+';">'	    	
    	+ '	<h6>Gadget content</h6>'		
    	+ '	<div class="mpages_gadget_framework_loading">'
    	+ '	<span class="mpages_gadget_framework_loading_icon"></span>'
    	+ '	<span class="mpages_gadget_framework_loading_text">Initializing...</span>'
    	+ '	</div>'
    	+ '</div>'
    	+ '</div>';
	
	//return the resulting html
	return(sHTML);
};

/*
	Function: decorateGadgets

	This prototype function decorates the gadgets by calling the init function of gadget holder.
	essentially it adds controls and initializes the JQuery sortable functions.
	
*/
GadgetContainer.prototype.decorateGadgets = function() {

	//initialize the gadget holder
	var oGadgetHolder = new GadgetHolder(this);
	
	//initialize the gadget holder
	oGadgetHolder.init();
};

/*
	Function: loadGadgets

	This prototype function dynamically pulls in our JS files for the gadgets based
	on the gadgetRendererLocation property

*/
GadgetContainer.prototype.loadGadgets = function() {
	
	// grab the gadget settings object
	var arGadgetSettings = this.getAllGadgetSettings();
	
	var scriptsAr = [];
	var gadgetIndexAr = [];
	
	var oHead = $('head');
	
	//loop through the gadget settings object
	for (var i = 0; i < arGadgetSettings.length; i++) {
		var gadgetSettings = arGadgetSettings[i];
		
		// if no type has been assigned, make it HTML
		if (gadgetSettings.gadgetType!=null
				&& gadgetSettings.gadgetType=="html") {
			
			continue;
		}
	
		// if the render location is null or empty, continue on
		if (gadgetSettings.gadgetRendererLocation==null
			|| gadgetSettings.gadgetRendererLocation=="") {
			continue;
		}
		
		scriptsAr.push(gadgetSettings.gadgetRendererLocation);
		
		gadgetIndexAr.push(i);
		
		//allow cross site scripting
		//jQuery.support.cors = true;
	
		//oHead.append("<script type='text/javascript' src='"+gadgetSettings.gadgetRendererLocation+"'></script>");
		
		jQuery.ajax({
			url: gadgetSettings.gadgetRendererLocation,
			async: false,
			cache: false,
			dataType: "script",
			success : function(data, textStatus, jqxhr) {	
	
			for (var i = 0; i < scriptsAr.length; i++){		

				if (this.url.indexOf(scriptsAr[i]) != -1) {
				
					initGadgetScript(arGadgetSettings, gadgetIndexAr[i], arGadgetSettings[gadgetIndexAr[i]], null);
				}
			}
			}
		});
		/*
		$.getScript(gadgetSettings.gadgetRendererLocation, function(data, textStatus, jqxhr) {	
	
			for (var i = 0; i < scriptsAr.length; i++){		

				if (this.url.indexOf(scriptsAr[i]) != -1) {
				
					initGadgetScript(arGadgetSettings, gadgetIndexAr[i], arGadgetSettings[gadgetIndexAr[i]], null);
				}
			}
		});
		*/
	}
	
};
		
/*
	Function: initGadgetScript

	This function is responsible for calling the init function and loadData for each of the gadget, it also initializes the callback that occurs after LoadData
	
	Parameters:
	
		arGadgetSettings - the settings array 
		
		index - not being used currently
		
		gadgetSettings - the individual gadget settings 
		
		scriptObj - the DOM id for the object?

*/

function initGadgetScript(arGadgetSettings, index, gadgetSettings, scriptObj){

	// grab the id for this gadget
	//var iValue = parseInt(scriptObj.id.substr(3,4));
	var iValue = index;
	// pull the appropriate settings from settings array
	var gadgetSettings = arGadgetSettings[iValue];
	
	//make sure it's not empty
	if (gadgetSettings.gadgetClassName!="") {
		
		// build the name using class name _ script id
		//var sName = gadgetSettings.gadgetClassName + "_" + iValue;
		var sName = window.gadgetContainer.getGadgetObjectNameById(gadgetSettings);
		//var sName = getGadgetObjectNameById_static(gadgetSettings);
		
		//create object
		eval(sName + ' = new ' + gadgetSettings.gadgetClassName + '();');
		
		//add object reference to component object
		eval('gadgetSettings.object = ' + sName + ';');
		eval('gadgetSettings.objectName = "' + sName + '";');
		
		//set properties
		eval(sName + '.gadgetClassName = "' + gadgetSettings.gadgetClassName + '";');
		eval(sName + '.gadgetObjectName = "' + sName + '";');
		eval(sName + '.gadgetId = "' + gadgetSettings.gadgetId + '";');
		
		//set callback for onLoadData
		var sCallback = 'function(){ ' +

			'if (' + sName + '.renderContent() == false){ ' + 
			'	$("#' + gadgetSettings.gadgetId + '").html("<div style=\'color:red;background-color:yellow;\'><span class=\'data_error\'></span> Error Occured while Loading ...</div>"); ' + 
			'}';
			
		if (index+1 < arGadgetSettings.length){
			//disable chaining the loading of gadgets

		}
		sCallback += '}';

		//initialize object with div id and settings
		eval(sName + '.initialize(gadgetSettings.gadgetConfigParameters);');		
		eval(sName + '.loadContent('+sCallback+')');
	}
	
}

function getGadgetObjectNameById_static(gadgetSettings) {
	return gadgetSettings.gadgetClassName + "_obj";
};
