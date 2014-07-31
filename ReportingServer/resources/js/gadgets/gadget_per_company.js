

var Gadget_Filter = function(){
	
	//this.oData;
	/*
	this.defaultOptions = {
		showComments: false,
		filter : "NO"
	};
	*/
    
};

//make a subclass of iGadget
Gadget_Filter.prototype = new Gadget();	
Gadget_Filter.prototype.constructor = Gadget;  
Gadget_Filter.prototype.base = Gadget.prototype;
Gadget_Filter.prototype.name = "Gadget_Filter";

//init
Gadget_Filter.prototype.initialize =function(options){
	//alert('in init');
	this.base.initialize.call(this,options);	
	// add your custom initialization here
	
	this.setGadgetProperty('attr1','value1');
	
	//alert('done init');
	return this;
};

//loadData		
Gadget_Filter.prototype.loadContent = function(callback){
	//alert('in loadContent');
	//extend options
	//invoke AJAX call	
	var thisComponent =this;
	
	$.ajax({
		url: "/employee.html",
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
Gadget_Filter.prototype.renderContent = function(target){
	//alert('in renderContent Gadget_Filter');
	this.renderAjaxResult();
	//alert('return true');
	return(true);
};

	
//NEW - draw data
Gadget_Filter.prototype.renderAjaxResult = function(){

	var thisComponent =this;
		
};
//@ sourceURL=Gadget_Filter.js

//refresh
Gadget_Filter.prototype.refreshContent =function(callback){
	//this.base.refresh.call(this,callback);
	clearVal_('company');
	//return this;
}