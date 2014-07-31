
var Gadget = function(){

	//this.combinedOptions = new Object();	
    this.gadgetId;
	this.gadgetClassName;
	this.gadgetObjectName;
	this.gadgetWidth;
	this.gadgetRowNumber;
	this.displayGadgetHeader;
	/*
    this.gadgetIFrameId;
    this.headerTitle;
    this.overFlowStyle = "tearoff";
    this.overFlowHeight = "120";
    this.columnNumber;

    this.gadgetConfigParameters = {};
	this.defaultOptions = {};
    */
};

//make a subclass of Gadget

Gadget.prototype.getGadgetProperty = function(sProperty){
	return eval("this."+sProperty);
};

Gadget.prototype.setGadgetProperty = function(sProperty, propValue){
	eval("this." + sProperty + "= '"+ propValue + "';");
};


//methods (public)
Gadget.prototype.initialize = function(initOptions){
	
	//extend options
	$.extend(true,this.gadgetConfigParameters,this.defaultOptions,initOptions);
	
	
	//REQUIRED - This must be called at the end of the Initialize() method
	this.onInitializeObject();
};


Gadget.prototype.loadContent = function(){

	//REQUIRED - This must be called at the end of the LoadContent() method
	this.onLoadContent();
};

Gadget.prototype.renderContent = function(responseText){
	
	//REQUIRED - This must be called at the end of the RenderContent() method
	this.onRenderContent();
};

Gadget.prototype.refreshContent = function(){
		//sHTML = '<span class="mpages_gadget_framework_loading_text">Refreshing ...</span>';
		sHTML = '<div style="text-align:center;float:left;"><img src="../resources/images/ajax-loader.gif" style="border:none;"></div>';
		$('#'+this.gadgetId).find(".gadget-content").html(sHTML);
		//set callback for onLoadData
		this.loadContent(renderGadgetContent);
};

Gadget.prototype.publishEvent = function(){
	
};

Gadget.prototype.consumeEvent = function(name){
	
};

Gadget.prototype.GetGadgetConfig = function(sProperty){
	return false;
};

Gadget.prototype.SetGadgetConfig = function(sProperty, propValue){
	return false;
};

//events (callback handlers)
Gadget.prototype.onInitializeObject = function(){ return null };

Gadget.prototype.onLoadContent = function(){ return null };

Gadget.prototype.onRenderContent = function(){ return null };

function renderGadgetContent(gadgetObj){
	//alert('re-rendering');
	if (gadgetObj.renderContent() == false){ 
		$("#"+gadgetObj.gadgetId).html("<div style=\'color:red;background-color:yellow;\'><span class=\'data_error\'></span> Error Occured while Loading ...</div>");  
	}
}
	