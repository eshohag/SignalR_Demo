"use strict";

var connection = new signalR.HubConnectionBuilder()
	.withUrl("/dashboard_push_notification_hub")
	//.withHubProtocol(new signalR.protocols.msgpack.MessagePackHubProtocol())
	.build();

$(function () {
	connection.start().then(function () {
		/*alert('Connected to dashboardHub');*/
		InvokeProducts();
	}).catch(function (err) {
		return console.error(err.toString());
	});
});

// Product
function InvokeProducts() {
	connection.invoke("RequestSummary").catch(function (err) {
		return console.error(err.toString());
	});
}

connection.on("ReceivedSummary", function (totalCount) {
	BindProductsToGrid(totalCount);
});

function BindProductsToGrid(totalCount) {
	document.getElementById("totalProduct").innerText = totalCount;
}