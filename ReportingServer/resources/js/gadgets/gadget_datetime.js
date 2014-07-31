

var Gadget_DateTime = function(){
	
	//this.oData;
	/*
	this.defaultOptions = {
		showComments: false,
		filter : "NO"
	};
	*/
    
};

//make a subclass of iGadget
Gadget_DateTime.prototype = new Gadget();	
Gadget_DateTime.prototype.constructor = Gadget;  
Gadget_DateTime.prototype.base = Gadget.prototype;
Gadget_DateTime.prototype.name = "Gadget_DateTime";

//init
Gadget_DateTime.prototype.initialize =function(options){
	//alert('in init');
	this.base.initialize.call(this,options);	
	// add your custom initialization here
	
	this.setGadgetProperty('attr1','value1');
	
	//alert('done init');
	return this;
};

//loadData		
Gadget_DateTime.prototype.loadContent = function(callback){
	//alert('in loadContent');
	//extend options
	//invoke AJAX call	
	var thisComponent =this;
	
	$.ajax({
		url: "/date_time.html",
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
Gadget_DateTime.prototype.renderContent = function(target){
	//alert('in renderContent Gadget_Filter');
	this.renderAjaxResult();
	//alert('return true');
	return(true);
};

	
//NEW - draw data
Gadget_DateTime.prototype.renderAjaxResult = function(){

	var thisComponent =this;
		
};
//@ sourceURL=Gadget_DateTime.js