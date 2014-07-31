
var Gadget_SystemHealth = function(){
	
	//this.oData;
    
};

//make a subclass of iGadget
Gadget_SystemHealth.prototype = new Gadget();	
Gadget_SystemHealth.prototype.constructor = Gadget;  
Gadget_SystemHealth.prototype.base = Gadget.prototype;
Gadget_SystemHealth.prototype.name = "Gadget_SystemHealth";

//init
Gadget_SystemHealth.prototype.initialize =function(options){
	//alert('in init');
	this.base.initialize.call(this,options);	
	// add your custom initialization here
	// any extra properties passed in "options" will now be on "this"
	// Default ccl and default cclParams are a good start
	
	// if (!this.ccl) this.ccl ="my_ccl"	
	this.setGadgetProperty('attr1','value1');
	
	//alert('done init');
	return this;
};

//loadData		
Gadget_SystemHealth.prototype.loadContent = function(callback){
	//alert('in loadContent');
	//extend options
	//invoke AJAX call	
	var thisComponent =this;

	//alert(this.getGadgetProperty('attr1'));
	var serviceUrl = "./referenceData";
	$.ajax({
		url: serviceUrl,
		dataType: 'xml',
		async: true,
		success: function(respData){			
			//alert(respData);
			thisComponent.data = respData;
			if (callback) callback(thisComponent);
		},
		error: function(XMLHttpRequest, textStatus, errorThrown){
 			alert("ERROR:= textStatus:" + textStatus + ", errorThrown: " + errorThrown);
		}
	});
	
};

// render
Gadget_SystemHealth.prototype.renderContent = function(target){
	//alert('in renderContent Gadget_SystemHealth');
	this.renderAjaxResult();
	//alert('return true');
	return(true);
};


//NEW - draw data
Gadget_SystemHealth.prototype.renderAjaxResult = function(){

	var thisComponent = this;
	sHTML = '';
	
	var sHTML = '<div style="margin-top:5px; text-align:left; float:left; width:30%;" id="solutionNamesSet">';
	//sHTML += '<select name="solutionName" id="solutionName">';
	if (this.data!=null){
		$(this.data).find("solution").each(function(){
            var solutionId = $(this).find("id:first").text();
            var solutionDesc = $(this).find("description:first").text();
			//sHTML += '<option value="'+solutionId+'">'+$(this).find("description:first").text()+'</option>';
            //sHTML += '<input type="checkbox" id="sol_'+solutionId+'" class="solutionNameButton" checked="checked" solId="'+solutionId+'"><label for="sol_'+solutionId+'">'+solutionDesc+'</label><br/>';
            //sHTML += '<input type="checkbox" class="solutionCheckbox" value="'+solutionId+'" id="sol_'+solutionId+'" name="'+solutionDesc+'"/>';
            sHTML += '<div class="squaredThree0" style="font-weight:bold;font-size:14px; clear:both;">';
            sHTML += '<div style="width:30%;float:left;"><input type="checkbox" value="'+solutionId+'" id="sol_'+solutionId+'" name="'+solutionDesc+'" checked="checked" class="solutionCheckbox" style="height:20px;width:20px;padding-right:10px;"/></div>';
            sHTML += '<div style="width:70%;float:left;"><label for="sol_'+solutionId+'" style="display:inline-block;vertical-align:middle;">'+solutionDesc+'</label></div>';
            sHTML += '</div>';
		});
	}
	//sHTML += '</select>';
    
	sHTML += '</div>';
	sHTML += '<div id="layerStatuses" style="float:right; width:65%; margin-right:5px;" class="ui-widget-content"></div>';
	
	//append
    //alert(this.gadgetId);
	$('#'+this.gadgetId).find(".gadget-content").html(sHTML);
	
	/*
	$('select#solutionName').selectmenu({
		width : 137
	});
    //$( "#solutionNamesSet" ).buttonset();
	$('#solutionNamesSet').buttonset().change(function() {
        // Remove icons from all buttons
        $('#solutionNamesSet input').button("option", "icons", {primary: ""});
        // Add icon to the selected button        
        $('input:checked', this).button("option", "icons", {primary: "ui-icon-check"}).button("refresh");
    });
    // Don't forgot to load the icon on the default option 
    $('input:checked', '#solutionNamesSet').button("option", "icons", {primary: "ui-icon-check"}).button("refresh");
    $('.solutionNameButton').live('click', function(){
    
	    var checkedSolutionIds = "";
	      $('.solutionNameButton').each(function(idx, elt){
	    	//alert($(this).attr('id')+" - "+$(this).attr('checked'));
	    	if($(elt).attr('checked')=='checked'){
	    		//alert($(this).attr('id'));
	            checkedSolutionIds += $(this).attr('solId')+",";
	    	}
	    });
	    
        Gadget_SystemHealth_obj.refreshSolutionLayersStatus(checkedSolutionIds);
    	
    });
    */
	//$('input.solutionCheckbox').prettyCheckable();
	
	$('.solutionCheckbox').live('click', function(){
		
	    var checkedSolutionIds = "";
	      $('.solutionCheckbox').each(function(idx, elt){
	    	//alert($(this).attr('id')+" - "+$(this).attr('checked'));
	    	if($(elt).attr('checked')=='checked'){
	    		//alert($(this).attr('id'));
	            checkedSolutionIds += $(this).attr('value')+",";
	    	}
	    });
	      
        Gadget_SystemHealth_obj.refreshSolutionLayersStatus(checkedSolutionIds);
    	
    });
    
    	var checkedSolutionIds = "";
	    $('.solutionCheckbox').each(function(idx, elt){
	    	//alert($(this).attr('id')+" - "+$(this).attr('checked'));
	    	if($(elt).attr('checked')=='checked'){
	    		//alert($(this).attr('id'));
	            checkedSolutionIds += $(this).attr('value')+",";
	    	}
	    });
	    
    Gadget_SystemHealth_obj.solutionIds = checkedSolutionIds;
    Gadget_SystemHealth_obj.getSolutionLayersStatus();
    
    Gadget_Filter_obj.solutionIds = checkedSolutionIds;
    Gadget_Filter_obj.refreshContent();
    
    Gadget_Notifications_obj.refreshContent();
    
    Gadget_Graphs_obj.refreshContent();
    
  	$(document).ready(function(){	
		setInterval(function(){Gadget_SystemHealth_obj.getSolutionLayersStatus();},15000);
	});
};

Gadget_SystemHealth.prototype.refreshSolutionLayersStatus = function(checkedSolutionIds){
	//alert("refreshing:"+checkedSolutionIds);
    Gadget_SystemHealth_obj.solutionIds = checkedSolutionIds;
    Gadget_SystemHealth_obj.getSolutionLayersStatus();
    
    Gadget_Filter_obj.solutionIds = checkedSolutionIds;
    Gadget_Filter_obj.refreshContent();
    
    Gadget_Notifications_obj.refreshContent();
    
    Gadget_Graphs_obj.refreshContent();
};
    
Gadget_SystemHealth.prototype.getSolutionLayersStatus = function(){
    
	//alert("getSolutionLayersStatus");
    var serviceUrl = "./solutionStatus";
    if(Gadget_SystemHealth_obj.solutionIds!="" && Gadget_SystemHealth_obj.solutionIds!=null){
        serviceUrl += "?solutionIds="+Gadget_SystemHealth_obj.solutionIds;
    }
    
	$.ajax({
		url: serviceUrl,
		dataType: 'xml',
		async: true,
		success: function(respData){			
			//alert(respData);
			//thisComponent.fireEvent("dataloaded");
			Gadget_SystemHealth_obj.renderSolutionLayers(respData);
		},
		error: function(XMLHttpRequest, textStatus, errorThrown){
 			alert("ERROR:= textStatus:" + textStatus + ", errorThrown: " + errorThrown);
		}
	});	
};
    
Gadget_SystemHealth.prototype.renderSolutionLayers = function(solutionLayersStatusData){
	
	var sHTML = '';
	sHTML += '<table id = "layer_table" cellpadding="0" cellspacing="0" border="0" width="99%">';
	//alert('data: '+this.data);
    //alert(solutionLayersStatusData);
	if (solutionLayersStatusData!=null && Gadget_SystemHealth_obj.solutionIds!=""){
		var count = 0;
		$(solutionLayersStatusData).find("layer").each(function(){
			count ++;
			if($(this).find("applicable").text() == "false") {
				sHTML += '<tr class="syshealth-row-disabled" solutionLayerId="'+$(this).find("id").text()+'" style="height:30px;';
				sHTML += 'background-color:#f0f0f0;';
				sHTML += '">';			
				sHTML += '<td colspan="2" style="border-bottom:1px solid #c0c0c0;"><span style="font:70% arial, helvetica, sans-serif; font-size:10pt;font-weight:bold;padding-left:5px;">'+$(this).find("description").text()+'</span></td>';
				sHTML += '</tr>';
			}else{
				sHTML += '<tr class="syshealth-row" solutionLayerId="'+$(this).find("id").text()+'" style="height:30px;';
				if(count%2==0){
					sHTML += 'background-color:#eaf1f7;';
				}
				sHTML += '" applicable="'+$(this).find("applicable").text()+'">';			
				sHTML += '<td style="border-bottom:1px solid #c0c0c0;"><span style="font:70% arial, helvetica, sans-serif; font-size:10pt;font-weight:bold;padding-left:5px;">'+$(this).find("description").text()+'</span></td>';
					if($(this).find("status").text() == "green") {
						sHTML += '<td style="border-bottom:1px solid #c0c0c0;"><img src="../resources/images/Green_Light_Icon.png" class="syshealth-icon" width="16" height="16" style="border:none;margin:7px;"></td>';
					}else if($(this).find("status").text() == "yellow"){ 
						sHTML += '<td style="border-bottom:1px solid #c0c0c0;"><img src="../resources/images/Blue_Light_Icon.png" class="syshealth-icon" width="16" height="16" style="border:none;margin:7px;"></td>';
					}else { 
						sHTML += '<td style="border-bottom:1px solid #c0c0c0;"><img src="../resources/images/Red_Light_Icon.png" class="syshealth-icon" width="16" height="16" style="border:none;margin:7px;"></td>';
					}
				sHTML += '</tr>';
			}
		});

	} else {
		$(solutionLayersStatusData).find("layer").each(function(){
			sHTML += '<tr class="syshealth-row-disabled" solutionLayerId="'+$(this).find("id").text()+'" style="height:30px;';
			sHTML += 'background-color:#f0f0f0;';
			sHTML += '">';			
			sHTML += '<td style="border-bottom:1px solid #c0c0c0;"><span style="font:70% arial, helvetica, sans-serif; font-size:10pt;font-weight:bold;padding-left:5px;">'+$(this).find("description").text()+'</span></td>';
			sHTML += '</tr>';
		});
	}
	sHTML += '</table>';
	//append
	$('#layerStatuses').html(sHTML);
	
	$(".syshealth-row").click(function(event) {
		//alert('filter for layer');
		//var trObj = $(event.target.parentNode.parentNode);
		//var trObj = $(event.target);
		var trObj = $(this);
		var layerId = trObj.attr('solutionLayerId');
		if(trObj.attr('applicable')=="false"){
			return;
		}
		$(".syshealth-row").each(function (){
			$(this).removeClass('rowSelected');
		});
		trObj.addClass('rowSelected');
		//alert(layerId);
		Gadget_Filter_obj.solutionLayerId = layerId;
		Gadget_Filter_obj.setFields();
		Gadget_Notifications_obj.refreshContent();
	});

	
  	//$(document).ready(function(){	
  	//	Gadget_SystemHealth_obj.updateStatus(Gadget_SystemHealth_obj.updateStatus);
	//});
	
};
/*
Gadget_SystemHealth.prototype.updateStatus = function(callback){
	
	var updateData;
				$.ajax({
		url: "./solutionStatus",
		dataType: 'xml',
		async: true,
		success: function(respData){			
			//alert(respData);
			updateData = respData;
			//thisComponent.fireEvent("dataloaded");

		},
		error: function(XMLHttpRequest, textStatus, errorThrown){
 			//alert("ERROR:= textStatus:" + textStatus + ", errorThrown: " + errorThrown);
		}
		});	
		setTimeout(function(){
		
			if(updateData != null)
			{
			var i =0;
			$(updateData).find('layer').each(function(){
						if($(this).find("status").text() == "green") {
				$('#layer_table').find('tr:eq('+i+')').find('td:eq(1)').html('<img src="../resources/images/Green_Light_Icon.png" class="syshealth-icon" width="16" height="16" style="border:none;margin:8px;">');
			}else if($(this).find("status").text() == "yellow"){ 
				$('#layer_table').find('tr:eq('+i+')').find('td:eq(1)').html('<img src="../resources/images/Blue_Light_Icon.png" class="syshealth-icon" width="16" height="16" style="border:none;margin:8px;">');
			}else { 
			$('#layer_table').find('tr:eq('+i+')').find('td:eq(1)').html('<img src="../resources/images/Red_Light_Icon.png" class="syshealth-icon" width="16" height="16" style="border:none;margin:8px;">');
			}
			i++;
			});
			}
			callback(callback);
			},30000);
};
*/
/*
//resize
Gadget_SystemHealth.prototype.resize =function(callback){
	this.base.resize.call(this,callback);
	
	return this;
}

//refresh
Gadget_SystemHealth.prototype.refreshContent =function(callback){
	this.base.refresh.call(this,callback);
	
	return this;
}

//destroy
Gadget_SystemHealth.prototype.destroy =function(callback){
	// add your custom cleanup here. Delete any listeners or expandos on 
	// DOM object, any memory structures created, etc 

	this.base.destroy.call(this,callback);
}
*/

//@ sourceURL=Gadget_SystemHealth.js
