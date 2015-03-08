<?php
include 'Score.php';
include 'ScoreDao.php';
include 'ScoreService.php';
header("Last-Modified: " . gmdate("D, d M Y H:i:s") . " GMT");
header("Cache-Control: no-store, no-cache, must-revalidate"); // HTTP/1.1
header("Cache-Control: post-check=0, pre-check=0", false);
header("Pragma: no-cache"); // HTTP/1.0
header("Expires: Sat, 26 Jul 1997 05:00:00 GMT"); // Date in the past

$DB_PDO = 'mysql:host=localhost;dbname=runshadow';
$DB_USER = 'USER';
$DB_PASSWORD = 'PASSWORD';

try {
  $DB = new PDO($DB_PDO, $DB_USER, $DB_PASSWORD);
  $DB->exec("set names utf8");

  $dao = new ScoreDao($DB);
  $service = new ScoreService($dao);

  if(isset($_GET['action'])) {
    
    if ($_GET['action'] == "add") {
      
      $device = "";
      if(isset($_GET['device'])) {
        $device = $_GET['device'];
      } else {
        $device = $service->getDeviceFromUserAgent($_SERVER['HTTP_USER_AGENT']);
      }
      
      $score = new Score();
      $score->name = $_GET['name'];
      $score->score = $_GET['score'];
      $score->device = $device;
      $score->isocountrycode = $service->getCountryCodeFromRemoteAddress($_SERVER['REMOTE_ADDR']);
      $score->useragent = $_SERVER['HTTP_USER_AGENT'];
      $score->characterplayed = isset($_GET['char']) ? $_GET['char'] : 0;

      if ($service->checkHash($score->name, $score->score, $_GET['hash'])) {
        $service->addScore($score);
      }
    } else if ($_GET['action'] == "list") {
      $gameVersion = isset($_GET['version']) ? $_GET['version'] : "1.0";
      
      $format = isset($_GET['format']) ? $_GET['format'] : "string";
      
      if (isset($_GET['period']) && $_GET['period'] == '24') {
        $scores = $service->getTopScores24hours($gameVersion);
      } else {
        $scores = $service->getTopScoresAllTime($gameVersion);
      }
      
      $scoresArray = $service->formatScoresArray($scores);
      
      if($format == "json") {
        
        header("Content-Type: application/json");
        echo json_encode($scoresArray);
        
      } else {
        
        foreach ($scoresArray as $row) {
          echo str_replace("|", "l", str_replace("-","–" ,$row[0])) . "-" . $row[1] . "|";
        }
      }

    } else {
      echo '{ "message" : "Invalid action"}';
    }
  } else {
     echo '{ "message" : "No action"}';
  }
} catch (Exception $e) {
    $protocol = (isset($_SERVER['SERVER_PROTOCOL']) ? $_SERVER['SERVER_PROTOCOL'] : 'HTTP/1.0');
    header($protocol . ' 500 Internal server error');
    http_response_code(500);
    echo 'connection error';
}