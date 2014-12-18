<?php
header("Last-Modified: " . gmdate("D, d M Y H:i:s") . " GMT");
header("Cache-Control: no-store, no-cache, must-revalidate"); // HTTP/1.1
header("Cache-Control: post-check=0, pre-check=0", false);
header("Pragma: no-cache"); // HTTP/1.0
header("Expires: Sat, 26 Jul 1997 05:00:00 GMT"); // Date in the past

if(isset($_GET['action']) && $_GET['action'] == "add") {
  $db = mysql_connect('localhost', 'USER', 'PASSWORD');
  mysql_set_charset('utf8',$db);
  
  if($db == false)
  {
    //http_response_code(500);
    die('Could not connect to server');// . mysql_error());
  }
  mysql_select_db('runshadow') or die('Could not select database');

  // Strings must be escaped to prevent SQL injection attack. 
  $name = mysql_real_escape_string($_GET['name'], $db); 
  
  if(trim($name) == ""){
    $protocol = (isset($_SERVER['SERVER_PROTOCOL']) ? $_SERVER['SERVER_PROTOCOL'] : 'HTTP/1.0');
    header($protocol . ' 500 Internal server error');
    //echo "okoaksfoa";
    die();
  }
  $score = mysql_real_escape_string($_GET['score'], $db);
  $useragent = mysql_real_escape_string($_SERVER['HTTP_USER_AGENT']);    
  $hash = $_GET['hash'];    
  $characterplayed = 0;
  if(isset($_GET['char'])){
    $characterplayed = mysql_real_escape_string($_GET['char'], $db);
  }
  
  $device = "webplayer";
  
  // detect the client by the user-agent from the http header
  if(strpos(strtolower($useragent),"windows phone") !== false){
    $device = "windowsphone";
  } else if (strpos(strtolower($useragent),"android") !== false) {
    $device = "android";
  } else if (strpos(strtolower($useragent),"ipad") !== false) {
    $device = "ios";
  } else if (strpos(strtolower($useragent),"iphone") !== false) {
    $device = "ios";
  } else if (strpos(strtolower($useragent),"ipod") !== false) {
    $device = "ios";
  } else if (strpos(strtolower($useragent),"cfnetwork") !== false) {
    $device = "ios";
  }
  
  
  $secretKey = "YOUR-SECRET"; # Change this value to match the value stored in the client 

  // api server to get user country based on IP
  $url_track = "http://ipinfo.io/" . $_SERVER['REMOTE_ADDR']."/country"; 
  $countrycode = @file_get_contents($url_track);
  
  if($countrycode) {
    $countrycode = "'" . mysql_real_escape_string($countrycode) . "'";
  } else {
    $countrycode = "NULL";
  }
  
  $real_hash = md5($name . $score . $secretKey); 
  if($real_hash == $hash) { 

    $query = "INSERT INTO scoreboard (name, score, timestamp, device, isocountrycode, useragent, characterplayed)
    VALUES ('$name', '$score', NOW(), '$device', $countrycode, '$useragent', '$characterplayed');"; 
    $result = mysql_query($query);
    
    if(!$result ){
      $protocol = (isset($_SERVER['SERVER_PROTOCOL']) ? $_SERVER['SERVER_PROTOCOL'] : 'HTTP/1.0');
      header($protocol . ' 500 Internal server error');
      die('Could not connect to server ' . mysql_error());//: ' . mysql_error());
    }
  } 
} else if (isset($_GET['action']) && $_GET['action'] == "list") {
    
  $game_version = "1.0";
  if(isset($_GET['version'])){
    $game_version = $_GET['version'];
  }
  
  $format = "string";
  if(isset($_GET['format'])){
    $format = $_GET['format'];
  }
      
  @$db = mysql_connect('localhost', 'USER', 'PASSWORD');
  mysql_set_charset('utf8',$db);
  
  if($db == false) {
    $protocol = (isset($_SERVER['SERVER_PROTOCOL']) ? $_SERVER['SERVER_PROTOCOL'] : 'HTTP/1.0');
    header($protocol . ' 500 Internal server error');
    $GLOBALS['http_response_code'] = 500;
    die('Could not connect to server');//: ' . mysql_error());
  }
  mysql_select_db('runshadow') or die('Could not select database');

  $query = "";
  
  if (isset($_GET['period']) && $_GET['period'] == "24") {
  
    // query for last 24 hours
    $query = "SELECT * , (
SELECT characterplayed
FROM  `scoreboard` AS player
WHERE board.maxscore = player.score
AND board.name = player.name
AND board.device <=> player.device
LIMIT 1
) AS characterplayed
FROM (

SELECT name, MAX( score ) AS maxscore, device, isocountrycode
FROM  `scoreboard` 
WHERE TIMESTAMP IS NOT NULL 
AND TIMESTAMP > DATE_SUB( NOW( ) , INTERVAL 24 HOUR ) 
AND TIMESTAMP <= NOW( ) 
GROUP BY name, device, isocountrycode, characterplayed
) AS board
ORDER BY  `maxscore` DESC 
LIMIT 5
";
   
  } else {
    $query = "SELECT s1.name, s1.score, s1.device, s1.isocountrycode, s1.characterplayed
FROM `scoreboard` AS s1
WHERE s1.TIMESTAMP IS NOT NULL 
AND s1.score = ( 
SELECT MAX( s2.score ) 
FROM `scoreboard` AS s2
WHERE s2.name = s1.name ) 
GROUP BY s1.name, s1.device, s1.isocountrycode
ORDER BY s1.score DESC 
LIMIT 5
";
  }
    
  $result = mysql_query($query) or die('Query failed: ' . mysql_error());

  $num_results = mysql_num_rows($result);  

  if($format == "json") {
    header("Content-Type: application/json");
    $data_array = array();
    for($i = 0; $i < $num_results; $i++) {
      $row = mysql_fetch_row($result);
      
      //if client version doesnt have character 6 return 0 for that player
      if(version_compare($game_version, "1.14", "<") && $row[4] > 5) {
        $row[4] = 0;
      }
      $data_array[] =  $row;        
    }
    
    // debug mode adding &debug=1 to url
    if(isset($_GET['debug']) && $_GET['debug'] == "1") {
      mysql_set_charset('utf8',$db);
      echo mysql_client_encoding();
    }
    
    echo json_encode($data_array);
    
  } else {
  
    // old version for old clients, not recommended because of charset and format problems, kept here for compatibility
    for($i = 0; $i < $num_results; $i++) {
      $row = mysql_fetch_row($result);
      echo str_replace("|", "l", str_replace("-","." ,$row[0])) . "-" . $row[1] . "|";    
    }
  }
} else {
  echo "No action";
}
?>