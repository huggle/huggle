<?php
/* Huggle WA - app/parse-rc.php */
	$ch2 = curl_init();
	$curlurl2 = "http://huggle.wmflabs.org/w/api.php?action=query&list=recentchanges&format=xml"; // ToDo: using results of loadwikis.php
	
	curl_setopt($ch2, CURLOPT_URL, $curlurl2);
	curl_setopt($ch2, CURLOPT_HEADER, 0);
	curl_setopt($ch2, CURLOPT_RETURNTRANSFER, 1);
	
	$rcxml = curl_exec($ch2);
	
	curl_close($ch2);
	
	$rcdata = new SimpleXMLElement($rcxml);
	
	// ToDo: part of analysing results
?>