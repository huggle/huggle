<?php
/* Huggle WA - apps/loadwikis.php */
	$ch = curl_init();
	$curlurl1 = "$gwikiprotocol$gwikiadress/index.php?action=raw&title=Huggle_WA/TestConf";

	curl_setopt($ch, CURLOPT_URL, $curlurl1);
	curl_setopt($ch, CURLOPT_HEADER, 0);
	curl_setopt($ch, CURLOPT_RETURNTRANSFER, 1);

	$wikis = curl_exec($ch);

	curl_close($ch);
	
	$conf = new SimpleXMLElement($wikis);

	$wikiname = $conf->wikiname[0];
	$wikiurl = $conf->wikiurl[0];

?>