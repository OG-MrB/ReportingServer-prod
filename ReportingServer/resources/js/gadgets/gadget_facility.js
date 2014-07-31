

var Gadget_Facility = function(){
	
	//this.oData;
	/*
	this.defaultOptions = {
		showComments: false,
		filter : "NO"
	};
	*/
    
};

//make a subclass of iGadget
Gadget_Facility.prototype = new Gadget();	
Gadget_Facility.prototype.constructor = Gadget;  
Gadget_Facility.prototype.base = Gadget.prototype;
Gadget_Facility.prototype.name = "Gadget_Facility";

//init
Gadget_Facility.prototype.initialize =function(options){
	//alert('in init');
	this.base.initialize.call(this,options);	
	// add your custom initialization here
	
	this.setGadgetProperty('attr1','value1');
	
	//alert('done init');
	return this;
};

//loadData		
Gadget_Facility.prototype.loadContent = function(callback){
	//alert('in loadContent');
	//extend options
	//invoke AJAX call	
	var thisComponent =this;
	
	$.ajax({
		url: "/portal.html",
		async: true,
		success: function(respData){		
			$('#'+thisComponent.gadgetId).find(".gadget-content").html(respData);
		},
		error: function(XMLHttpRequest, textStatus, errorThrown){
 			alert("ERROR:= textStatus:" + textStatus + ", errorThrown: " + errorThrown);
		}
	});
};

// render
Gadget_Facility.prototype.renderContent = function(target){
	//alert('in renderContent Gadget_Filter');
	this.renderAjaxResult();
	//alert('return true');
	return(true);
};

	
//NEW - draw data
Gadget_Facility.prototype.renderAjaxResult = function(){

	var thisComponent =this;
		
};
//@ sourceURL=Gadget_Facility.js

//refresh
Gadget_Facility.prototype.refreshContent =function(callback){
	//this.base.refresh.call(this,callback);
	clearVal_('facility');
	//return this;
}