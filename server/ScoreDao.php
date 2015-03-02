<?php
class ScoreDao {
  
  private $connection;
  
  public function __construct($connection) {
    if (!$connection) {
      throw new Exception('No connection found');
    }
    $this->connection = $connection;
  }
  
  public function addScore($score) {
    $this->connection->prepare('
      INSERT INTO scoreboard (name, score, timestamp, device, isocountrycode, useragent, characterplayed)
      VALUES (?, ?, NOW(), ?, ?, ?, ?);
    ')->execute(array($score->name, $score->score, $score->device, $score->isocountrycode, $score->useragent, $score->characterplayed));
  }
  
  public function getTopScores24hours() {
    //name,score,device,isocountrycode,characterplayed
    $query = "
    SELECT * , (
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
    
    $scores = array();
    
    foreach ($this->connection->query($query) as $row) {
      $score = new Score();
      $score->name = $row['name'];
      $score->score = $row['maxscore'];
      $score->device = $row['device'];
      $score->isocountrycode = $row['isocountrycode'];
      $score->characterplayed = $row['characterplayed'];
      
      $scores[] = $score;
    }    
    
    return $scores;
  }
  
  public function getTopScoresAllTime() {
    //name,score,device,isocountrycode,characterplayed
    $query = "
    SELECT s1.name, s1.score, s1.device, s1.isocountrycode, s1.characterplayed
    FROM `scoreboard` AS s1
    WHERE s1.TIMESTAMP IS NOT NULL 
    AND s1.score = ( 
      SELECT MAX( s2.score ) 
      FROM `scoreboard` AS s2
      WHERE s2.name = s1.name 
    ) 
    GROUP BY s1.name, s1.device, s1.isocountrycode
    ORDER BY s1.score DESC 
    LIMIT 5
    ";
    
    $scores = array();
    
    foreach ($this->connection->query($query) as $row) {
      $score = new Score();
      $score->name = $row['name'];
      $score->score = $row['score'];
      $score->device = $row['device'];
      $score->isocountrycode = $row['isocountrycode'];
      $score->characterplayed = $row['characterplayed'];
      
      $scores[] = $score;
    }    
    
    return $scores;
  }
}