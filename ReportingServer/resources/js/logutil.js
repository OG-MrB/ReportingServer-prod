jQuery.extend({
	logInfo: function(msg) {
		if (window.console) {
			// Firefox & Google Chrome
			console.log(new Date()+": INFO - "+msg);                     // Write the logs under "Logs" section
		} else {
			// Other browsers
			//$("body").append("<div style=\"width:600px;color:#FFFFFF;background-color:#000000;\">" + msg + "</div>");
		}
		return true;
	},
	logError: function(msg) {
		if (window.console) {
			// Firefox & Google Chrome
			console.error(new Date()+": ERROR - "+msg);                  // Write the logs under "Errors" section
		} else {
			// Other browsers
			//$("body").append("<div style=\"width:600px;color:#FFFFFF;background-color:#000000;\">" + msg + "</div>");
		}
		return true;
	}
});