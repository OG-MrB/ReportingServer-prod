
var Gadget_Alerts = function(){
	
	//this.oData;
	this.defaultOptions = {
		editable: false
	};
	
};

//make a subclass of iGadget
Gadget_Alerts.prototype = new Gadget();	
Gadget_Alerts.prototype.constructor = Gadget;  
Gadget_Alerts.prototype.base = Gadget.prototype;
Gadget_Alerts.prototype.name = "Gadget_Alerts";

//init
Gadget_Alerts.prototype.initialize =function(options){
	//alert('in init');
	this.base.initialize.call(this,options);	
	// add your custom initialization here
	// any extra properties passed in "options" will now be on "this"
	this.setGadgetProperty('attr1','value1');
	
	//alert('done init');
	return this;
};

//loadData		
Gadget_Alerts.prototype.loadContent = function(callback){
	//alert('in loadContent');
	//extend options
	//invoke AJAX call
	var thisComponent =this;
	
	$.ajax({
		url: "/misc.html",
		async: true,
		success: function(respData){		
			$('#'+thisComponent.gadgetId).find(".gadget-content").html(respData);
		},
		error: function(XMLHttpRequest, textStatus, errorThrown){
 			alert("ERROR:= textStatus:" + textStatus + ", errorThrown: " + errorThrown);
		}
	});

	//if (callback) callback(thisComponent);
};

// render
Gadget_Alerts.prototype.renderContent = function(target){
	//alert('in renderContent Gadget_Alerts');
	this.renderAjaxResult();
	//alert('return true');
	return(true);
};
	
//NEW - draw data
Gadget_Alerts.prototype.renderAjaxResult = function(){
	
	var thisComponent =this;
/*
	var sHTML = '<div style="margin-top:5px; text-align:center;">';
	sHTML += '<select name="reportType" id="reportType" disabled="true"><option value="">Select a Report</option><option value="R01">Report 1</option><option value="R01">Report 2</option></select>';
	sHTML += '</div>';
		
	//append
	$('#'+this.gadgetId).find(".gadget-content").html(sHTML);
	$('select#reportType').selectmenu({
		width : 137
	}); */
	
};

//resize
Gadget_Alerts.prototype.resize =function(callback){
	this.base.resize.call(this,callback);
	
	return this;
}

//refresh
Gadget_Alerts.prototype.refreshContent =function(callback){
	//this.base.refresh.call(this,callback);
	clearVal_('misc1');
	clearVal_('misc2');
	//return this;
}

//destroy
Gadget_Alerts.prototype.destroy =function(callback){
	// add your custom cleanup here. Delete any listeners or expandos on 
	// DOM object, any memory structures created, etc 

	this.base.destroy.call(this,callback);
}

//@ sourceURL=Gadget_Alerts.js		