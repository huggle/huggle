<?php
/* Huggle WA - app/loadwikis.php */
	$ch1 = curl_init();
	$curlurl1 = $gwikiprotocol.$gwikiadress."/index.php?action=raw&title=Huggle_WA/TestConf";

	curl_setopt($ch1, CURLOPT_URL, $curlurl1);
	curl_setopt($ch1, CURLOPT_HEADER, 0);
	curl_setopt($ch1, CURLOPT_RETURNTRANSFER, 1);

	$wikis = curl_exec($ch1);

	curl_close($ch1);
	
	$conf = new SimpleXMLElement($wikis);

	$wikiname1 = $conf->wikiname[0];
	$wikiurl1 = $conf->wikiurl[0];

?>