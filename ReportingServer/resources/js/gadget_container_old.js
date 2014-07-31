var GadgetContainer = function(){

	/****************** PRIVATE VARIABLE ***********************/
			
	var arGadgetSettings = new Array();
	var arDisplayColumns = new Array();		
	
	var defaultGadgetSettings = {
        movable: true,
        removable: true,
        collapsible: true,
        editable: true,
		collapsed : false,
		
		//Determines header values
		//headerIconFile : "",
		headerTitle : "MPages Gadget",
		headerToolbarText : "",
		gadgetStyleClassName : "",
		
		//Determines what happens when the gadget overflows vertically
		//overFlowStyle : "none",     //none, scroll, tearoff
		//overFlowHeight : "100",
		//overFlowHeightSlop : 20,
		
		gadgetType : "script",   //object, script, html
		gadgetRendererLocation : "",
		gadgetClassName : "",
		gadgetConfigParameters : {},
		
		//column or div to render in
		columnNumber :  1,
		//"customDivId" : "",
		
		//private properties
		gadgetId : "",
		containerId : "",
		object : undefined,
		objectName : ""
	};
	
	//this.init : function () {
		//
	//};
	
	this.getGadgetSettings = function (id) {
		var $ = this.jQuery,
			settings = this.settings;
		return 'Gadget Display Content for '+id;
	};
	
	//this.alignRows = false;
	
	//this.threadMax = 2;
	this.columnCnt = 1;
	this.aColumnWidth = new Array();
	
	this.frameworkDivId = "mpages_gdaget_framework";
	this.frameworkStyle = "column";
	
	//this.colorScheme = "gray";

	//add a gadget to the container
	this.addGadget = function(sGadgetSettings){
		//merge the gadget settings and the defaults
		var combinedGadgetsettings = new Object();
		$.extend(true, combinedGadgetsettings, defaultGadgetSettings, sGadgetSettings);
		
		//add gadget to array
		arGadgetSettings.push(combinedGadgetsettings);
	};
	
	this.renderGadgetColumns = function(){
	        
		//create the column arrays from the gadget list
		createDisplayColumnArray(this);			
		//array to hold column width
		var aWidth = getColumnWidth(this);			
		//outer div
		var sHTML = '<div id="columns">';
						
		//Get the max column length
		//var iMaxCnt = GetMaxColumnLength();
			
		//loop through columns
		for (var i = 0; i < this.columnCnt; i++){		
			//add column
			var colIndex = i + 1;
			sHTML += '<ul id="column'+colIndex+'" index="'+colIndex+'" class="column" style="vertical-align:top;width:' + aWidth[i] + ';">';				
			//loop through each columns gadgets
			for (var j = 0; j < arDisplayColumns[i].length; j++ ){
				//add gadget 
				//may consider using gadget holder
				sHTML += this.getGadgetHtml(arDisplayColumns[i][j], this);
			}				
			//end list
			sHTML += '</ul>';	
		}	
		//end div	
		sHTML += '</div>';			
		//draw on page
		$("#" + this.frameworkDivId).html(sHTML);
		
	};


	/****************** PRIVATE METHODS ***********************/
	//create the aColumn array with the reference to each gadget
	var createDisplayColumnArray = function(me){
		//create a sub-array for each column
		for (var i = 0; i < me.columnCnt; i++){
			arDisplayColumns[i] = new Array();
		}
		
		//loop throught the gadgets and assign them to each column
		for (var iGadget = 0; iGadget < arGadgetSettings.length; iGadget++){
			var iColumn = arGadgetSettings[iGadget].columnNumber;
			if (iColumn <= me.columnCnt){
				//next index
				var iCount = arDisplayColumns[iColumn-1].length;
				
				//reference to Gadget by index
				arDisplayColumns[iColumn-1][iCount] = iGadget;
			}
		}
	};
	
	//get the longest columns length
	var getMaxColumnLength = function(me){
		var iMax = 0;
		for (var i = 0; i < arDisplayColumns.length; i++){
			if (arDisplayColumns[i].length > iMax){
				iMax = arDisplayColumns[i].length;
			}
		}
	};
	
	//get column width array
	var getColumnWidth = function(me){
		//check if aColumnWidth defined appropriately
		if (me.aColumnWidth.length == me.columnCnt){
			return(me.aColumnWidth);
		} else {
			//split evenly as percentage
			var sWidth = Math.round(100/me.columnCnt) + "%";
			var aWidth = new Array(me.columnCnt);
			for (var i = 0; i < aWidth.length; i++){
				aWidth[i] = sWidth;
			}
			return(aWidth);
		}
	};
		
	this.getGadgetSettingsByIndex = function(iGadget){
		return arGadgetSettings[iGadget];
	};
	
	this.getGadgetSettingsById = function(gadgetId){
		for (var iGadget = 0; iGadget < arGadgetSettings.length; iGadget++){
			if(gadgetId == arGadgetSettings[iGadget].gadgetId){
				return arGadgetSettings[iGadget];
			}
		}		
	};
	
	//create the aColumn array with the reference to each gadget
	this.getAllGadgetSettings = function(){
		return arGadgetSettings;
	};
};


GadgetContainer.prototype.getGadgetHtml = function(iGadget, me){
	//reference to Gadget configuration object
	var oGadget = this.getGadgetSettingsByIndex(iGadget);

	//initialize html variable
	var sHTML = "";

	oGadget.gadgetId = "gd-" + iGadget;
	
	sHTML += '<li class="gadget '+oGadget.gadgetStyleClassName+'" id="'+oGadget.gadgetId+'">'  
    	+ '<div class="gadget-head">'
    	+ '<h3>'+oGadget.headerTitle+'</h3>'
    	+ '</div>'
    	+ '<div class="gadget-content" style="height:200px: overflow:scroll-y">'	    	
    	+ '	<h6>Gadget content</h6>'		
    	+ '	<div class="mpages_gadget_framework_loading">'
    	+ '	<span class="mpages_gadget_framework_loading_icon"></span>'
    	+ '	<span class="mpages_gadget_framework_loading_text">Initializing...</span>'
    	+ '	</div>'
    	+ '</div>'
    	+ '</li>';
	
	/*
	//overflow style (default height to auto and assign later)
	var sBodyOverflowStyle = "";
	var bTearOff = false;
	if (oGadget.overFlowStyle == "scroll" && oGadget.overFlowHeight != ""){
		sBodyOverflowStyle = "overflow-y:auto;height:auto;";
	} else if (oGadget.overFlowStyle == "tearoff" && oGadget.overFlowHeight != ""){
		sBodyOverflowStyle = "overflow-y:hidden;height:auto;";
		bTearOff = true;
	} else {
		sBodyOverflowStyle = "height:auto";
	}
	*/
	
	//title hyperlink
	/*
	var sTitleHyperlink = "";
	if (oGadget.headerTitleTab != "" && me.personId > 0 && me.encntrId > 0){
		sTitleHyperlink = "javascript:APPLINK(0,'powerchart.exe','/PERSONID="+me.personId+" /ENCNTRID="+me.encntrId+" /FIRSTTAB=^" + oGadget.headerTitleTab + "^');";
	}
	*/
				
	//color scheme
	//var sColorSchemeClass = "mpages_gadget_framework_" + me.colorScheme;
	
	//tearoff footer
	/*
	if (bTearOff == true){
		sHTML += '<div id="' + oGadget.containerId + '_footer" class="mpages_gadget_framework_footer_tearoff" onClick="oMPagesSummaryFramework.ToggleCutOff(\'' + oGadget.containerId + '\');">' +
					'<div class="mpages_gadget_framework_footer_tearoff_left"></div>' + 
					'<div class="mpages_gadget_framework_footer_tearoff_more">More...</div>' + 
					'<div class="mpages_gadget_framework_footer_tearoff_right"></div>' + 
				'</div>';
	}
	*/
	
	//return HTML
	return(sHTML);
};


GadgetContainer.prototype.decorateGadgets = function(){

	var oGadgetHolder = new GadgetHolder(this);
	oGadgetHolder.init();
};


GadgetContainer.prototype.loadGadgets = function(){
	
	var arGadgetSettings = this.getAllGadgetSettings();
	var oHead = $('head');
	
	for (var i = 0; i < arGadgetSettings.length; i++){
		var gadgetSettings = arGadgetSettings[i];

		if(gadgetSettings.gadgetType!=null
				&& gadgetSettings.gadgetType=="html"){
			/*
			$.ajax({
				url: gadgetSettings.gadgetRendererLocation,
				dataType: 'html',
				async: true,
				success: function(html){			
					//alert(html);
					$('#gd-1').find(".gadget-content").html(html);	
				},
				error: function(XMLHttpRequest, textStatus, errorThrown){
		 			alert("ERROR:= textStatus:" + textStatus + ", errorThrown: " + errorThrown);
				}
			});
			*/	
			continue;
		}
		//else type is script
		
		if(gadgetSettings.gadgetRendererLocation==null
			|| gadgetSettings.gadgetRendererLocation==""){
			continue;
		}
		
		var html_doc = document.getElementsByTagName('head')[0];
		oScript = document.createElement('script');
		oScript.setAttribute('type', 'text/javascript');
		oScript.setAttribute('src', gadgetSettings.gadgetRendererLocation);
		oScript.setAttribute('id', 'sc-'+i);
	    html_doc.appendChild(oScript);
		    
        //alert('loading ... '+gadgetSettings.gadgetRendererLocation);
		
		oScript.onreadystatechange = function () {
	        //alert(oScript.readyState);
	        if (oScript.readyState == 'complete' || oScript.readyState == 'loaded') {
	            //alert('JS onreadystate fired');
	            initGadgetScript(arGadgetSettings, i, gadgetSettings, this);
	        }
	    }

		oScript.onload = function () {
	        initGadgetScript(arGadgetSettings, i, gadgetSettings, this);
	    }
		
		//doesn't work
		//var oScript = $('<script id="sc-'+i+'" type="text/javascript" src="'+gadgetSettings.gadgetRendererLocation+'"></script>');
		//oScript.appendTo('head');
		//oScript.bind("load", initGadgetScript(arGadgetSettings, gadgetSettings, this.id));
	}
	
	//execute LoadData on first Object
	//eval(arGadgetSettings[0].objectName + '.LoadContent()');
};

function initGadgetScript(arGadgetSettings, index, gadgetSettings, scriptObj){

	//alert("script loaded: "+scriptObj.id);
	var iValue = parseInt(scriptObj.id.substr(3,4));
	var gadgetSettings = arGadgetSettings[iValue];
	if(gadgetSettings.gadgetClassName!=""){
		var sName = gadgetSettings.gadgetClassName + "_" + iValue;
		
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
		//var sCallback = sName + '.onLoadData = function(){ ' +
		var sCallback = 'function(){ ' +
			//'alert("in on load"); ' +  
			//sName + '.render();';
			'if (' + sName + '.render() == false){ ' + 
			'	$("#' + gadgetSettings.gadgetId + '").html("<div style=\'color:red;background-color:yellow;\'><span class=\'data_error\'></span> Error Occured while Loading ...</div>"); ' + 
			'}';
			
		if (index+1 < arGadgetSettings.length){
			//disable chaining the loading of gadgets
			//sCallback += arGadgetSettings[i+1].objectName + '.loadData();';
		}
		sCallback += '}';
		//eval(sCallback);
		
		//initialize object with div id and settings
		eval(sName + '.init(gadgetSettings.gadgetConfigParameters);');		
		eval(sName + '.loadData('+sCallback+')');
	}

}
