
var Gadget_Data = function(){
	
	//this.oData;
	this.defaultOptions = {
		editable: false
	};
	
};

//make a subclass of iGadget
Gadget_Data.prototype = new Gadget();	
Gadget_Data.prototype.constructor = Gadget;  
Gadget_Data.prototype.base = Gadget.prototype;
Gadget_Data.prototype.name = "Gadget_Data";

//init
Gadget_Data.prototype.initialize =function(options){
	//alert('in init');
	this.base.initialize.call(this,options);	
	// add your custom initialization here
	// any extra properties passed in "options" will now be on "this"
	this.setGadgetProperty('attr1','value1');
	
	//alert('done init');
	return this;
};

//loadData		
Gadget_Data.prototype.loadContent = function(callback){
	//alert('in loadContent');
	//extend options
	//invoke AJAX call
	var thisComponent =this;
	
	$.ajax({
		url: "/ReportView.html",
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
Gadget_Data.prototype.renderContent = function(target){
	//alert('in renderContent Gadget_Data');
	this.renderAjaxResult();
	//alert('return true');
	return(true);
};
	
//NEW - draw data
Gadget_Data.prototype.renderAjaxResult = function(){
	
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
Gadget_Data.prototype.resize =function(callback){
	this.base.resize.call(this,callback);
	
	return this;
}

//refresh
Gadget_Data.prototype.refreshContent =function(callback){
	$('#gridContainer div').html('');
}

//destroy
Gadget_Data.prototype.destroy =function(callback){
	// add your custom cleanup here. Delete any listeners or expandos on 
	// DOM object, any memory structures created, etc 

	this.base.destroy.call(this,callback);
}

//@ sourceURL=Gadget_Data.js		