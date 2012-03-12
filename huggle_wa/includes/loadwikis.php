<?php
//This is a source code or part of Huggle project
//
//This file contains code for index page
//last modified by Petrb

//Copyright (C) 2011-2012 Huggle team

//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.

//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.
/* Huggle WA - app/loadwikis.php */

if ( !defined( 'HUGGLE' ) ) {
	echo "This is a part of huggle wa, unable to load config";
	die (1);
}

/* class Wiki {
	public static function LoadWikis() {
		global $hgwa_WikiProtocol, $hgwa_WikiAddress;
		$ch1 = curl_init();
		$curlurl1 = $hgwa_WikiProtocol.$hgwa_WikiAddress."/index.php?action=raw&title=Huggle_WA/TestConf";

	curl_setopt($ch1, CURLOPT_URL, $curlurl1);
	curl_setopt($ch1, CURLOPT_HEADER, 0);
	curl_setopt($ch1, CURLOPT_RETURNTRANSFER, 1);

	$wikis = curl_exec($ch1);

	curl_close($ch1);
	
	$conf = new SimpleXMLElement($wikis);

	$wikiname1 = $conf->wikiname[0];
	$wikiurl1 = $conf->wikiurl[0];
	}
}
