
var Gadget_Graphs = function(){
	
	//this.oData;
	/*
	this.defaultOptions = {
		showComments: false,
		filter : "NO"
	};
	*/
    
};

//make a subclass of iGadget
Gadget_Graphs.prototype = new Gadget();	
Gadget_Graphs.prototype.constructor = Gadget;  
Gadget_Graphs.prototype.base = Gadget.prototype;
Gadget_Graphs.prototype.name = "Gadget_Graphs";

//init
Gadget_Graphs.prototype.initialize =function(options){
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
Gadget_Graphs.prototype.loadContent = function(callback){
	//alert('in loadContent');
	//extend options
	//invoke AJAX call	
	var thisComponent =this;

	var systemHealthGadgetObj = Gadget_SystemHealth_obj;
	//alert(systemHealthGadgetObj.solutionIds);
	$.ajax({
		url: "./notificationsSummaryGraph"+"?solutionIds="+systemHealthGadgetObj.solutionIds,
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
Gadget_Graphs.prototype.renderContent = function(target){
	//alert('in renderContent Gadget_Graphs');
	this.renderAjaxResult();
	//alert('return true');
	return(true);
};
	
//NEW - draw data
Gadget_Graphs.prototype.renderAjaxResult = function(){
	sHTML = '';
	
	var sHTML = '<div style="margin-top:5px; text-align:left;">';
	sHTML += '<select name="graphType" id="graphType"><option value="NOTIFICATIONS">Notifications</option></select>';
	sHTML += '</div>';
	sHTML += '<div style="clear: both"></div>';
	sHTML += '<div id="notificationsByLayer" style="float: left; margin-top: 0px; margin-left: 50px;">';
	sHTML += '<div id="columnChart" class="graph"></div>';
	sHTML += '</div>';
	
	//append
	$('#'+this.gadgetId).find(".gadget-content").html(sHTML);
	$('select#graphType').selectmenu({
		width : 137
	});
	
	
	var ntfStats =new Array();
	
	if (this.data!=null){
		var index = 0;
		$(this.data).find("item").each(function(){
		
			var layerName = $(this).find("solutionLayer").find("description").text();
			idx = layerName.indexOf("Layer");
			if(idx>0){
				layerName = layerName.substr(0,idx);
			}
			var count = $(this).find("notificationCount").text();
			ntfStats[index] = new Array(); 
			ntfStats[index][0] = "<td class='graph_row_style' onclick=\"filterForbutton('"+index+"-"+layerName+"')\">"+layerName+"</td><td class='graph_row_style' onclick=\"filterForbutton('"+layerName+"')\">"+count+"</td>";
			ntfStats[index][1] = parseInt(count);
			index++;
			
		});
	}

	ntfStats.sort(function(a,b){
		if(a[0] < b[0]){
			return -1;
		}
		if(a[0] > b[0]){
			return 1;
		}
		return 0;
	});
	var col = new Array("#FFA500","#FE6B6B","#FFFF80","#A2EF81","#9CF5EB","#9CACF5","#BFB7BB","#E6E6FA","#E37DEC");

	var dataE = [];
	var names = [];
	if (this.data!=null){
		var index = 0;
		$(this.data).find("item").each(function(){
		
			dataE.push({data: [[index,ntfStats[index][1]]],
     bars: {
                show: true,
				barWidth: 0.1,
               
                fill: true,
                fillColor: col[index]
            },

			color: col[index]});
			names.push([index, $(this).find("solutionLayer").find("description").text()]) ;
			
			
			index++;
		});
	}

	 $.plot($("#columnChart"), dataE, {
     xaxis: { 
			ticks: names,
			tickLength: 0,
            axisLabelUseCanvas: true,
            axisLabelFontSizePixels: 8,
            axisLabelFontFamily: 'Verdana, Arial, Helvetica, Tahoma, sans-serif',
            axisLabelPadding: 5
			},
			   series: {
            lines: { show: true },
            points: {
                radius: 4,
                show: true,
                fill: true
            },
        },
        grid: { hoverable: true, clickable: true }
      });
//	$.plot($("#pieChart"), data,
	//{
	//	series: {
	//		pie: {
	//			show: true,
	//			radius: 0.80,
	//		}
	//	},
	//	legend: {
	//		show: true,
	//	},
	//	grid: {
  //       	hoverable: true,
  //       	clickable: true
  //      }
//	});
				//set time out in 1 min
		

};

/*
//resize
Gadget_Graphs.prototype.resize =function(callback){
	this.base.resize.call(this,callback);
	
	return this;
}

//refresh
Gadget_Graphs.prototype.refreshContent =function(callback){
	this.base.refresh.call(this,callback);
	
	return this;
}

//destroy
Gadget_Graphs.prototype.destroy =function(callback){
	// add your custom cleanup here. Delete any listeners or expandos on 
	// DOM object, any memory structures created, etc 

	this.base.destroy.call(this,callback);
}
*/

//@ sourceURL=Gadget_Graphs.js