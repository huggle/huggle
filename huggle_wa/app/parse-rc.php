<?php
/* Huggle WA - app/parse-rc.php */
	$ch2 = curl_init();
	$curlurl2 = $wikiurl1."/api.php?action=query&list=recentchanges&format=xml";

	curl_setopt($ch2, CURLOPT_URL, $curlurl2);
	curl_setopt($ch2, CURLOPT_HEADER, 0);
	curl_setopt($ch2, CURLOPT_RETURNTRANSFER, 1);
	
	$rcxml = curl_exec($ch2);
	
	curl_close($ch2);
	
	$rcdata = new SimpleXMLElement($rcxml);
	
	$rcid = $rcdata->query->recentchanges->rc[0]->attributes()->rcid;
	$rctitle = $rcdata->query->recentchanges->rc[0]->attributes()->title;
	$revid = $rcdata->query->recentchanges->rc[0]->attributes()->revid;
	$oldrevid = $rcdata->query->recentchanges->rc[0]->attributes()->old_revid;
	$timestamp = $rcdata->query->recentchanges->rc[0]->attributes()->timestamp;
	
	$rcarray = array(
	"rcid" => $rcid,
	"rctitle" => $rctitle,
	"revid" => $revid,
	"oldrevid" => $oldrevid,
	"timestamp" => $timestamp
	);
	
?>