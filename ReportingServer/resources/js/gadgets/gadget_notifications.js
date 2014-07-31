
	var Gadget_Notifications = function(){
		
		//this.oData;
		
	};

	//make a subclass of iGadget
	Gadget_Notifications.prototype = new Gadget();	
	Gadget_Notifications.prototype.constructor = Gadget;  
	Gadget_Notifications.prototype.base = Gadget.prototype;
	Gadget_Notifications.prototype.name = "Gadget_Notifications";

	//init
	Gadget_Notifications.prototype.initialize =function(options){
		//alert('in init');
		this.base.initialize.call(this,options);	
		// add your custom initialization here
		
		this.setGadgetProperty('attr1','value1');
		Gadget_Notifications_obj.stopTimeOut = [];
		//alert('done init');
		return this;
	};

	//loadData		
	Gadget_Notifications.prototype.loadContent = function(callback){
		//alert('in loadContent');
		//extend options
		//invoke AJAX call	
		var thisComponent = this;
		//alert(this.getGadgetProperty('attr1'));
		
		var ntfServiceUrl = Gadget_Notifications_obj.prepareNtfServiceUrl();
		//alert(ntfServiceUrl);
		
		$.ajax({
			url: ntfServiceUrl +"&startPos=1&limit=100",
			dataType: 'xml',
			async: true,
			success: function(respData){			
				//alert(respData);
				thisComponent.data = respData;
				//thisComponent.fireEvent("dataloaded");
				if (callback) callback(thisComponent);
			},
			error: function(XMLHttpRequest, textStatus, errorThrown){
				alert("ERROR:= textStatus:" + textStatus + ", errorThrown: " + errorThrown);
			}
		});		
		
	};

	// render
	Gadget_Notifications.prototype.renderContent = function(target){
		//alert('in renderContent Gadget_Notifications');
		this.renderAjaxResult();

		//alert('return true');
		return(true);
	};
		
	//NEW - draw data
	Gadget_Notifications.prototype.renderAjaxResult = function(){
		var thisComponent = this;
		var sHTML = '';

					

		//alert('data: '+this.data);
		//check length	
		if (this.data!=null){
				
			sHTML += '<table id="table_results" class="display" cellpadding="0" cellspacing="1" border="0" width="100%">';
			sHTML += '<thead>';
			sHTML += '<tr>';
			sHTML += '<th style="text-align: center;">Resource</th>';
			sHTML += '<th style="text-align: center;">Identifier</th>';
			//sHTML += '<th style="text-align: center1;">Reason</th>';
			sHTML += '<th style="text-align: center;">Failure Date</th>';
			sHTML += '<th style="text-align: center;">Failure Time</th>';
			sHTML += '<th style="text-align: center;">Recovery Date</th>';
			sHTML += '<th style="text-align: center;">Recovery Time</th>';
			sHTML += '<th style="text-align: center;">Service Status</th>';
			sHTML += '<th style="text-align: center;">Service Date</th>';
			sHTML += '<th style="text-align: center;">Service Time</th>';
			//sHTML += '<th style="text-align: center;">Ticket</th>';
			sHTML += '<th style="text-align: center;">Status</th>';
		
			sHTML += '<th style="text-align: center;">Log</th>';
			sHTML += '</tr>';
			sHTML += '</thead>';
			sHTML += '<tbody>';
			
			$(this.data).find("notification").each(function(){
				sHTML += '<tr notificationId="'+$(this).find("notificationId").text()+'" issueId ="'+$(this).find("issueId").text()+'">';
				sHTML += '<td>'+$(this).find("component").find("description").text()+'</td>';
				sHTML += '<td>'+$(this).find("hostname").text()+'</td>';
				//var compState = $(this).find("componentState").find("description").text();
			//	if(!compState || compState == ''){
			//		compState = "Down";
		//		}
			//	sHTML += '<td>'+compState+'</td>';
				sHTML += '<td>'+$(this).find("failureDateAsString").text()+'</td>';
				sHTML += '<td>'+$(this).find("failureTimeAsString").text()+'</td>';
				sHTML += '<td>'+$(this).find("recoveryDateAsString").text()+'</td>';
				sHTML += '<td>'+$(this).find("recoveryTimeAsString").text()+'</td>';
				sHTML += '<td>'+$(this).find("issueStatus").text()+'</td>';
				sHTML += '<td>'+$(this).find("issueCreateDateAsString").text()+'</td>';
					sHTML += '<td>'+$(this).find("issueCreateTimeAsString").text()+'</td>';
				//sHTML += '<td>'+$(this).find("issueId").text()+'</td>';
				sHTML += '<td class="center">';
				if($(this).find("notificationType").text()=='Problem'){
					sHTML += '<img id="ntfSearch" src="../resources/images/Flag-red.png" height="16px" width="16px" style="float:none;border:none;" value="3"';
				}else if($(this).find("notificationType").text()=='Recovery'){
					sHTML += '<img id="ntfSearch" src="../resources/images/Flag-blue.png" height="16px" width="16px" style="float:none;border:none;" value="2"';
				}else if($(this).find("notificationType").text()=='Acknowledgement'){
					sHTML += '<img id="ntfSearch" src="../resources/images/Flag-yellow.png" height="16px" width="16px" style="float:none;border:none;" value="1"';
				}
				sHTML += ' title = "'+ $(this).find("notificationType").text()+'" />';
				sHTML += '</td>';
				sHTML += '<td class="center">';
				sHTML += '<div class="view-info" style="font-size:0px;">';
				sHTML += '<img src="../resources/images/Notes-icon_large.png" width="30" height="30" style="text-align:center;border:none;" class="view-info-icon"/>';
				sHTML += '</div></td>';
				sHTML += '</tr>';
			});
		
			sHTML += '</tbody>';
			sHTML += '</table>';

			sHTML += '<div id="dialog" title="Notification Detail">';
			sHTML += '<p id="ntfCompDetails" style="font-weight:bold;"></p><div id="notificationAuditDetails"></div>';
			sHTML += '</div>';

		} else {
			sHTML = '<div class="nodata">No Info Available</div>';	
		}

		//append
		$('#'+this.gadgetId).find(".gadget-content").html(sHTML);
		
		//adjust datatable width
		//$('#'+this.gadgetId).find(".gadget-content").css("width","1015px");
		
		var oTable = $('#table_results').dataTable(
			{"aLengthMenu": [[5,10,25,100,-1], [5,10,25,100,"All"]],
			"iDisplayLength": -1,
			"sScrollY" : "490",
			"aaSorting" : [[2, "desc"]],
			"bJQueryUI": true,
			"bPaginate": false,
			"bLengthChange": false,
			"bSortClasses": false,
			"bFilter": false,
			"aoColumnDefs": [
				{ "bSortable": false, "aTargets": [ 3,5,6,9,10 ] }
			],
			"bAutoWidth": false,
			"aoColumns": [
				{ "sWidth": "20%" },
				{ "sWidth": "20%" },
				
				{ "sWidth": "8%" },
				{ "sWidth": "8%" },
				{ "sWidth": "8%" },
				{ "sWidth": "8%" },
				{ "sWidth": "8%" },
				{ "sWidth": "8%" },
				{ "sWidth": "8%" },
				{ "sWidth": "5%" },
				{ "sWidth": "5%" }
			]
			});
			//alert($(oTable.fnGetNodes()[0]).attr("notificationId"));
				
				//alert($(oTable.fnGetNodes()[99]).attr("notificationId"));			

		$("#table_results tbody").click(function(event) {
			$(oTable.fnSettings().aoData).each(function (){
				$(this.nTr).removeClass('row_selected');
			});
			var trObj = $(event.target.parentNode);
			trObj.addClass('row_selected');
			var ntfId = trObj.attr('notificationId');
			if(ntfId>0){
				thisComponent.selectedNotificationId = ntfId;
				Gadget_Alerts_obj.refreshContent();
			}
		});
		$(".view-info-icon").click(function(event) {
			//alert('show info');
			var trObj = $(event.target.parentNode.parentNode.parentNode);
			var ntfId = trObj.attr('notificationId');
			var issueId = trObj.attr('issueId');
			//alert(ntfId);
			thisComponent.selectedNotificationId = ntfId;
			thisComponent.selectedNofIssueId = issueId;
			var compName = $(trObj).find("td::nth-child(1)").text();
			var compInstance = $(trObj).find("td::nth-child(2)").text();
			var compState = $(trObj).find("td::nth-child(10)").find('img').attr('title');
			if(ntfId>0){
				Gadget_Alerts_obj.refreshContent();
				Gadget_Notifications_obj.loadNotificationAuditDetails(compName, compInstance, compState);
			}
		});
		
				$( "#dialog" ).dialog({
					height: 650,
					width: 800,
					modal: true,
					autoOpen: false
				});
					
				//set time out in 1 min
		$(document).ready(function(){
				if( Gadget_Notifications_obj.stopTimeOut != 1)
				{
					var timeOutId = setTimeout(function(){Gadget_Notifications_obj.updateNotifications(oTable,30000);},30000);
					Gadget_Notifications_obj.stopTimeOut.push(timeOutId);
					}
		});
	};


	Gadget_Notifications.prototype.loadNotificationAuditDetails = function(compName, compInstance, compState){

		var thisComponent = this;
		$("#ntfCompDetails").html(compName+" - "+compInstance+" : "+compState);
			
		var ntfServiceUrl = "./notificationAudit?notificationId="+thisComponent.selectedNotificationId+"&issueId=";
			if(thisComponent.selectedNofIssueId != null)
			{
		
			ntfServiceUrl+= thisComponent.selectedNofIssueId;
			}
		$.ajax({
			url: ntfServiceUrl,
			dataType: 'xml',
			async: true,
			success: function(respData){			
				//alert(respData);
			//	alert('click');
				var sHTML = '<br/>';
				
		if (respData!=null){				
				// Service Ticket
		if($(respData).find('allowedForTicketDetails').text() == 'true'){	//SCOPE		
		sHTML+= '<div><form><fieldset>';
	   sHTML+= '<legend> <h4>Service Detail</h4></legend>';
	
		sHTML+= '<input type="text" id = "issueid" value="Not Available" disabled >';
		sHTML+= '&nbsp<input type ="button" id ="geneTicket"  style="font-size:10px" value ="Generate Ticket!">';
		
		if(thisComponent.selectedNofIssueId =="" &&  (compState !="Recovery") )
		{
		
		sHTML+= '&nbsp<input type ="button" id ="resolved" style="font-size:10px" class="button" value ="Manual Recovery" >';
		}
	
		sHTML+= '</br></br><div id ="wrapper" style ="height : 140px;"><div style ="display:block;float:left;">';
		sHTML+= '<label style ="display:inline-block; width : 130px;" >Service Status</label><span><select id = "inputStat" class ="input_normal" disabled >';
		sHTML+= '<option value=""></option>';
		sHTML+= '<option value="1">New</option>';
		sHTML+= '<option value="2">In Progress</option>';
		sHTML+= '<option value="3">Resolved</option>';
		sHTML+= '<option value="5">Closed</option></select></span></br>';
		sHTML+= '<label style ="display:inline-block; width : 130px;"  >Service Priority</label><span><input type="text" id = "inputPrior" class ="input_normal" value="Not Available" disabled></span></br>';
		sHTML+= '<label style ="display:inline-block; width : 130px;" >Service Start</label><span><input type="text" id = "startdate" class ="input_time" value="Not Available" disabled > at <input type="text" id = "starttime" class ="input_time" value="Not Available" disabled></span></br>';
		sHTML+= '<label style ="display:inline-block; width : 130px;" >Service Complete</label><span><input type="text" id = "enddate" class ="input_time" value="Not Available" disabled> at <input type="text" id = "endtime" class ="input_time" value="Not Available" disabled></span></br>'
	   sHTML+= ' <input type="button" id = "updateTicket" style="font-size:10px" class="button" value ="Update" disabled></div>';
	   
		sHTML+= '<div style ="display:block;float: left;padding-left:15px;">';
		sHTML+= '<label style ="display:inline-block; width : 70px;">Assign</label><span><select id = "inputAssign" class = "input_normal" disabled>';
		sHTML+= '<option value=""></option>';

		
		sHTML+= '</select></span></br><label style ="display:inline-block; width : 70px;">Info</label></br><label style ="display:inline-block; width : 70px;"></label>';
		sHTML+= '<textarea rows ="2" cols="25" id = "description" style ="resize: none;" disabled >Not Available</textarea></div></div>';
					sHTML+= '<div id = "note_wrapper" style = "height : 90px">';
					sHTML += '<table id="ntf_note" class="display" cellpadding="0" cellspacing="1" border="0" width = "100%">';
					sHTML += '<thead >';
					sHTML += '<tr>';
					sHTML += '<th id ="clickA" style="text-align: center;">Date</th>';
					sHTML += '<th style="text-align: center;">Time</th>';
					sHTML += '<th style="text-align: center;">Note</th>';	
						sHTML += '<th style="text-align: center;">Update By</th></tr></thead >';						
					sHTML += '<tbody>'
					if($(respData).find('note').length >0)
					{
					//$(respData).find('note').each(function(){
					
				
					sHTML += '<tr'+$(respData).find('note').eq(0).find('noteId').text() + '>';
						sHTML += '<td>'+$(respData).find('note').eq(0).find('createdDateAsString').eq(0).text() + ' </td>';
					sHTML += '<td>'+$(respData).find('note').eq(0).find('createdTimeAsString').eq(0).text() + ' </td>';
					sHTML += '<td>'+$(respData).find('note').eq(0).find('notes').text()+' </td>';
					sHTML += '<td>'+$(respData).find('note').eq(0).find('username').text()+' </td>';
					sHTML +=  '</tr>';

					//});
					}
					
						sHTML += '<tbody></table></div>';
					
					
					sHTML +=  '<div style ="width : 99%"><div style ="display:block;float: left; width: 93%"><textarea placeholder="Write a note..." id="note" rows ="1"  style ="word-wrap: break-word;resize: none; width: 98%"></textarea></div>';
					sHTML +=  '<div style ="display:block;float: left;"><input type="button" id = "updateNote" class="button" value ="Post" cellpadding="0"></div></div></div>';
						//else
						//{
						//sHTML+= '<input type="text" value="Not Available" disabled>';
						//sHTML+= '<button id ="geneTicket" type ="button" > Generate Ticket! </button></br></br>';
						//sHTML+= '<div>No Information Available</div>';
						//}
		 //<textarea></textarea>
	   // <input type="radio">
	   // <input type="reset">
		
		
		sHTML+= '</fieldset></form></div>';
			
			}
			//SCOPE
				// Notification Log in Details
			
					sHTML += "&nbsp<h4>Log Details</h4>";
					sHTML += '<table id="ntf_audit_details" class="display" cellpadding="0" cellspacing="1" border="0" width = "100%">';
					sHTML += '<thead >';
					sHTML += '<tr>';
					sHTML += '<th id ="clickA" style="text-align: center;">Date</th>';
					sHTML += '<th style="text-align: center;">Time</th>';
					sHTML += '<th style="text-align: center;">Resource State</th>';
					sHTML += '<th style="text-align: center;">Details</th>';
					sHTML += '</tr>';
					sHTML += '</thead>';
					sHTML += '<tbody>';
					
					$(respData).find("notificationAudit").each(function(){
						sHTML += '<tr>';
						sHTML += '<td>'+$(this).find("createdDateAsString").text()+'</td>';
						sHTML += '<td>'+$(this).find("createdTimeAsString").text()+'</td>';
						sHTML += '<td>'+$(this).find("resourceState").find("description").text()+'</td>';
						sHTML += '<td>'+$(this).find("details").text()+'</td>';
						sHTML += '</tr>';
					});
				
					sHTML += '</tbody>';
					sHTML += '</table>';
				
				sHTML += '<div id="confirm-popup" ></div>';
				sHTML += '<div id="confirmMessage" style="display: none;"><center><h3>Updated successfully</h3></center></div>';
					sHTML += '<div id="loadingMessage" style="display: none;"><center><h3>Updating .... </h3></center></div>';
					sHTML += '<div id="errorMessage" style="display: none;"><center><h3>Failed to Update</h3></center></div>';
				} else {
					sHTML = '<div class="nodata">No Info Available</div>';	
				}
			
				//append
				$('#notificationAuditDetails').html(sHTML);
				
				
		$("#confirm-popup").dialog({
		autoOpen : false,
		height : 100,
		width : 300,
		position: [$(window).width()/2-150,$(window).height()*1/3-50],
		modal : true
	});
	//$('#updateTicket').button();
	//$('#geneTicket').button();
	//$('#resolved').button();
	if($(respData).find('allowedForTicketDetails').text() == 'true')
	{
				//issue details binding	
			Gadget_Notifications_obj.bindIssueDetails(respData);	
				// checking for resolve
				
					if(thisComponent.selectedNofIssueId =="" )
					{
					
					 if(! (compState =="Recovery"))
					 {
							$('#resolved').bind('click',function (){
							$("#confirm-popup").html($("#loadingMessage").html());
							$("#confirm-popup").dialog("open");
							
						$.ajax({
						url: "./resolveNotification?&notificationId="+thisComponent.selectedNotificationId,
						dataType: 'xml',
						async: true,
						success: function(resData){
						$("#confirm-popup").html($("#confirmMessage").html());	
						//		$("body").click(function(e){
						//		$("#confirm-popup").dialog("close");
						//		});
							},
						error: function(XMLHttpRequest, textStatus, errorThrown){
							//alert("ERROR:= textStatus:" + textStatus + ", errorThrown: " + errorThrown);
							$("#confirm-popup").html($("#errorMessage").html());
						 }
						 });	
						

						});
							
						}
						else
						{
						
						$('#description').val('Resolved prior to futher action');
						
						}
						
						}
						$('#resolved').bind('click',function (){
						$('#resolved').prop('disabled', true);
						});
				// Generate Ticket
				$('#geneTicket').bind('click',function (){
						
						$("#confirm-popup").html($("#loadingMessage").html());
						$("#confirm-popup").dialog("open");
						
						$.ajax({
						url: "./issueTicket?&notificationId="+thisComponent.selectedNotificationId,
						dataType: 'xml',
						async: true,
						success: function(resData){
								if (resData!=null){
										
									Gadget_Notifications_obj.bindIssueDetails(resData);
									
									thisComponent.selectedNofIssueId = $('#issueid').val();
								$("#confirm-popup").html($("#confirmMessage").html());
							//	$("body").click(function(e){
							//	$("#confirm-popup").dialog("close");});
								$('#geneTicket').prop('disabled', true);
								}
		
							},
						error: function(XMLHttpRequest, textStatus, errorThrown){
							//alert("ERROR:= textStatus:" + textStatus + ", errorThrown: " + errorThrown);
							$("#confirm-popup").html($("#errorMessage").html());
						}
					});	
						
								
								
					//$( "#dialog" ).dialog("close");		
				//$( document ).ready(function() {
					//Gadget_Notifications_obj.loadNotificationAuditDetails(compName, compInstance, compState,thisComponent.issueId);
					
				//	});
			
				});
				
				// Update Ticket
				$('#updateTicket').bind('click',function (){
							$("#confirm-popup").html($("#loadingMessage").html());
							$("#confirm-popup").dialog("open");
							
					$.ajax({
						url: "./updateTicket?&issueId="+thisComponent.selectedNofIssueId+"&status="+$('#inputStat').val()+"&priority="+"&assign="+$('#inputAssign').val()+"&resourceState="+"&alertDetails="+"&notificationId="+thisComponent.selectedNotificationId,
						dataType: 'xml',
						async: true,
						success: function(resData){			
								if (resData!=null){
										
									Gadget_Notifications_obj.bindIssueDetails(resData);	
									$("#confirm-popup").html($("#confirmMessage").html());
									
								}
							},
						error: function(XMLHttpRequest, textStatus, errorThrown){
							//alert("ERROR:= textStatus:" + textStatus + ", errorThrown: " + errorThrown);
							$("#confirm-popup").html($("#errorMessage").html());
						}
					});	
					//	$("body").click(function(e){
						//		$("#confirm-popup").dialog("close");});
								
				//Gadget_Notifications_obj.loadNotificationAuditDetails(compName, compInstance, compState);
				});
				var oTableSub = $('#ntf_note').dataTable({
				
				"iDisplayLength": 1,
				"iDisplayStart": 1,
				
			//  "sScrollX" : "",
				"bJQueryUI": true,
				"bPaginate": false,
				// "bLengthChange": false,
				"bFilter": false,
				//"bSort": false,
				 "bInfo": false,		
				//"bSortClasses": false,
					
					"aoColumnDefs": [
				{ "bSortable": false, "aTargets": [ 2,3 ] }],
				"bAutoWidth": false,
					"aoColumns": [
					{sWidth : '10%'},
					{sWidth : '15%'},
					{sWidth : '55%'},
					{sWidth : '20%'}

							]
    	});
			$('#updateNote').bind('click', function(){
					if(!($('#note').val() == '' || $('#note').val() ==null))
					{
									$.ajax({
						url: "./createNote?&notificationId="+thisComponent.selectedNotificationId+"&noteString="+$('#note').val(),
						dataType: 'xml',
						async: true,
						success: function(resData){			
								if (resData!=null ){
								$(resData).find('note').each(function(){
								if(oTableSub.fnSettings().fnRecordsTotal() > 0)
								{
								var addedIndex = oTableSub.fnUpdate(
								[
								$(this).find("createdDateAsString").eq(0).text(),
								$(this).find("createdTimeAsString").eq(0).text(),
								$(this).find('notes').text(),
								$(this).find('username').text()
								],0,false,false);
								$(oTableSub.fnGetNodes()[0]).attr("noteId", $(this).find("noteId").text());
								}
								else if (oTableSub.fnSettings().fnRecordsTotal() == 0)
								{
								
								var addedIndex = oTableSub.fnAddData(
								[
								$(this).find('notes').text(),
								$(this).find('username').text()
								],true);
								$(oTableSub.fnGetNodes()[addedIndex]).attr("noteId", $(this).find("noteId").text());
								}
								});
								
								}
				
		
							},
						error: function(XMLHttpRequest, textStatus, errorThrown){
							alert("ERROR:= textStatus:" + textStatus + ", errorThrown: " + errorThrown);
						}
					});	
					$('#note').val('');
					 //var oSettings = oTableSub.fnSettings();
					// oSetting._iDisplayStart = 1;
					//oSetting._iDisplayLength = 1:
					}
			});

	}
				var oTable = $('#ntf_audit_details').dataTable(
					{"aLengthMenu": [[5,10,25,100,5], [5,10,25,100,"All"]],
					
					"iDisplayLength": 5,
					//"sScrollY" : "90",
						
				"bJQueryUI": true,
					"bPaginate": false,
					//"bLengthChange": false,
				"bScrollCollapse": true,
					"bFilter": false,
					  "bScrollCollapse": true,
					"aaSorting" : [[0, "desc"]],
					"bInfo": false,
					"bAutoWidth": false,
				
					"aoColumns": [
						{sWidth : '100px'},
						{sWidth : '90px'},
						{sWidth : '100px'},
						{sWidth : '300px'}
							]
					});
				
				// oTable.fnAdjustColumnSizing();

				$( "#dialog" ).dialog("open");
				$("#dialog").bind("dialogclose", function(event) {
		// Gadget_Notifications_obj.refreshContent();
	//	Gadget_Notifications_obj.updateNotifications(oTable,1000,Gadget_Notifications_obj.updateNotifications);
	
	 });
			},
			error: function(XMLHttpRequest, textStatus, errorThrown){
			//	alert("ERROR:= textStatus:" + textStatus + ", errorThrown: " + errorThrown);
			}
		});		
		
	};
	
	Gadget_Notifications.prototype.prepareNtfServiceUrl = function ()
	{
	var ntfServiceUrl = "./notifications?";
		var filterGadgetObj = Gadget_Filter_obj;
		if(filterGadgetObj && filterGadgetObj.searchCriteriaSelected){
			ntfServiceUrl = "./searchNotifications?";
				if(filterGadgetObj.solutionLayerId)
					ntfServiceUrl += "&solutionLayerId="+filterGadgetObj.solutionLayerId;
				else
					ntfServiceUrl += "&solutionLayerId=";
				if(filterGadgetObj.componentId)
					ntfServiceUrl += "&componentId="+filterGadgetObj.componentId;
				else
					ntfServiceUrl += "&componentId=";
					ntfServiceUrl +="&fromDate=";
				if(filterGadgetObj.ntfFromDate )
					ntfServiceUrl += filterGadgetObj.ntfFromDate;
					ntfServiceUrl +="&toDate=";
					if (filterGadgetObj.ntfToDate)
					ntfServiceUrl += filterGadgetObj.ntfToDate;
		
				if(	filterGadgetObj.notificationType)
					ntfServiceUrl +=	"&notificationType="+filterGadgetObj.notificationType;
				else
					ntfServiceUrl +=	"&notificationType=";

		}
        if(filterGadgetObj.solutionIds)
            ntfServiceUrl += "&solutionIds="+filterGadgetObj.solutionIds;
        else
            ntfServiceUrl += "&solutionIds=";
        
		return ntfServiceUrl;
	};
	
	Gadget_Notifications.prototype.updateNotifications = function (oTable, delayTime)
	{	

			for(var i = 0; i <	Gadget_Notifications_obj.stopTimeOut.length;i++)
			 clearTimeout(Gadget_Notifications_obj.stopTimeOut[i]);

            var thisComponent = this;
            var updateData;
            var ntfServiceUrl = Gadget_Notifications_obj.prepareNtfServiceUrl();
			$.ajax({
			url: ntfServiceUrl+"&startPos=1&limit=100",
			dataType: 'xml',
			async: true,
			success: function(respData){			
				
				updateData = respData;
							var i = 0;
			oTable = $('#table_results').dataTable();
			var rows = oTable.fnGetNodes();
			
			$(updateData).find("notification").each(function(){
				// insert part
				
				//alert ( oTable.fnGetData(0,1)[3] < $(this).find("failureTimeAsString").text());
				  var temp = $(rows[i]).attr("notificationId");
			
				
							var sHTML = '';
					if($(this).find("notificationType").text()=='Problem'){
					sHTML += '<img id="ntfSearch" src="../resources/images/Flag-red.png" height="16px" width="16px" style="float:none;border:none;" value="3"';
				}else if($(this).find("notificationType").text()=='Recovery'){
					sHTML += '<img id="ntfSearch" src="../resources/images/Flag-blue.png" height="16px" width="16px" style="float:none;border:none;" value="2"';
				}else if($(this).find("notificationType").text()=='Acknowledgement'){
					sHTML += '<img id="ntfSearch" src="../resources/images/Flag-yellow.png" height="16px" width="16px" style="float:none;border:none;" value="1"';
				}
				sHTML += ' title = "'+ $(this).find("notificationType").text()+'" />';
				//alert((oTable.fnGetData(0,1))[9]);
				
				oTable.fnUpdate( $(this).find("component").find("description").text(),i,0,false,false);
				oTable.fnUpdate( $(this).find("hostname").text(), i,1,false,false);
				oTable.fnUpdate( $(this).find("failureDateAsString").text(), i,2,false,false);
				oTable.fnUpdate( $(this).find("failureTimeAsString").text(), i,3,false,false);
				oTable.fnUpdate( $(this).find("recoveryDateAsString").text(),i,4,false,false);
				oTable.fnUpdate( $(this).find("recoveryTimeAsString").text(),i,5,false,false);
				oTable.fnUpdate( $(this).find("issueStatus").text(),i,6,false,false);
				oTable.fnUpdate( $(this).find("issueCreateDateAsString").text(),i,7,false,false);
				oTable.fnUpdate( $(this).find("issueCreateTimeAsString").text(),i,8,false,false);
				oTable.fnUpdate(	sHTML,i,9,false,false);
				$(rows[i]).addClass('center');
				$(rows[i]).attr("notificationId", $(this).find("notificationId").text());
				$(rows[i]).attr("issueId",$(this).find("issueId").text());
				
					i++;
			});
				//thisComponent.fireEvent("dataloaded");
               
				var timeOutId = setTimeout(function(){Gadget_Notifications_obj.updateNotifications(oTable,delayTime);},delayTime);
				Gadget_Notifications_obj.stopTimeOut.push(timeOutId);
                
			},
			error: function(XMLHttpRequest, textStatus, errorThrown){
				//alert("ERROR:= textStatus:" + textStatus + ", errorThrown: " + errorThrown);
			}
		});


				// update part
			
			
		//	callback(oTable,delayTime,callback);
	  //alert(oTable.fnSettings().fnRecordsTotal());
	
		
		
		  //   setTimeout(function(){
		   //   Gadget_SystemHealth_obj.refreshContent(); },62000);
	 //  setTimeout(function(){
			 // Gadget_Graphs_obj.refreshContent(); },70000);
	
		
	};
	
Gadget_Notifications.prototype.bindIssueDetails = function(respData){
				if(	$(respData).find("issue") && $(respData).find("issue").length > 0 )
						{	
							
							$(respData).find("issue").each(function(){
							
							$('#issueid').val($(this).find("id:first").text());
							$('#geneTicket').prop('disabled', true);
							
							$('#inputStat').prop('disabled', false);
							$('#inputStat').val( $(this).find("status").attr("id"));
							
							$('#inputPrior').val($(this).find("priority").attr("name"));
							$('#startdate').val($(this).find("startdate").text());
							$('#starttime').val($(this).find("starttime").text())
							$('#enddate').val($(this).find("enddate").text());
							$('#endtime').val($(this).find("endtime").text());
							$('#updateTicket').prop('disabled', false);
							$('#updateTicket').removeAttr("disabled");
							
							$('#inputAssign').prop('disabled', false);
							$('#inputAssign option[value!=""]').remove();
							if($(this).find("assigned_to").attr("name") == null)
							{
							$('#inputAssign').append($('<option>', { value : 0 }).text('Unassigned')); 
							$('#inputAssign').val(0);
							}
							if($(this).find("assigned_to").attr("name") != null)
							{
							
							$('#inputAssign').append($('<option>', { value : $(this).find("assigned_to").attr("id") }).text($(this).find("assigned_to").attr("name"))); 
							$('#inputAssign').val($(this).find("assigned_to").attr("id"));
							}				
							$('#description').val($(this).find("description").text());
							$(respData).find("membership").each(function(){
									
										$('#inputAssign').append($('<option>', { value : $(this).find("user").attr("id") }).text($(this).find("user").attr("name"))); 
									});	
						});
						}
};
	/*
	//resize
	Gadget_Notifications.prototype.resize =function(callback){
		this.base.resize.call(this,callback);
		
		return this;
	}

	//refresh
	Gadget_Notifications.prototype.refreshContent =function(callback){
		this.base.refresh.call(this,callback);
		
		return this;
	}

	//destroy
	Gadget_Notifications.prototype.destroy =function(callback){
		// add your custom cleanup here. Delete any listeners or expandos on 
		// DOM object, any memory structures created, etc 

		this.base.destroy.call(this,callback);
	}
	*/		

	//@ sourceURL=Gadget_Notifications.js
