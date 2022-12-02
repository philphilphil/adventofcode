use crate::base::{Problem, ProblemData};
use lazy_static::lazy_static;
use std::collections::HashMap;

pub struct Day2 {}

#[derive(PartialEq, Hash, Eq)]
enum Move {
    Rock,
    Paper,
    Scicor,
}

lazy_static! {
    static ref POINTS: HashMap<Move, u32> = {
        let mut m = HashMap::new();
        m.insert(Move::Rock, 1);
        m.insert(Move::Paper, 2);
        m.insert(Move::Scicor, 3);
        m
    };
}

impl Problem for Day2 {
    fn part_one(&self, problem_data: &ProblemData) -> String {
        let mut score = 0;
        for line in problem_data.input.lines() {
            let moves: Vec<&str> = line.split(char::is_whitespace).collect();
            let enemy_move = string_to_move(moves[0]);
            let my_move = string_to_move(moves[1]);

            // always add move score
            score += POINTS[&my_move];

            // check for draw
            if my_move == enemy_move {
                score += 3;
                continue;
            }

            // check for win
            if (enemy_move == Move::Rock && my_move == Move::Paper)
                || (enemy_move == Move::Paper && my_move == Move::Scicor)
                || (enemy_move == Move::Scicor && my_move == Move::Rock)
            {
                score += 6
            }
        }

        score.to_string()
    }

    fn part_two(&self, problem_data: &ProblemData) -> String {
        let mut score = 0;
        for line in problem_data.input.lines() {
            let moves: Vec<&str> = line.split(char::is_whitespace).collect();
            let enemy_move = string_to_move(moves[0]);
            let outcome = string_to_outcome(moves[1]);

            match outcome {
                Outcome::Win => {
                    match enemy_move {
                        Move::Rock => score += POINTS[&Move::Paper],
                        Move::Paper => score += POINTS[&Move::Scicor],
                        Move::Scicor => score += POINTS[&Move::Rock],
                    };
                    score += 6;
                }
                Outcome::Loss => {
                    match enemy_move {
                        Move::Rock => score += POINTS[&Move::Scicor],
                        Move::Paper => score += POINTS[&Move::Rock],
                        Move::Scicor => score += POINTS[&Move::Paper],
                    };
                }
                Outcome::Draw => {
                    score += POINTS[&enemy_move];
                    score += 3;
                }
            }
        }

        score.to_string()
    }
}

fn string_to_move(move_str: &str) -> Move {
    match move_str {
        "A" | "X" => Move::Rock,
        "B" | "Y" => Move::Paper,
        "C" | "Z" => Move::Scicor,
        _ => panic!("no"),
    }
}
enum Outcome {
    Win,
    Loss,
    Draw,
}
fn string_to_outcome(outcome_str: &str) -> Outcome {
    match outcome_str {
        "X" => Outcome::Loss,
        "Y" => Outcome::Draw,
        "Z" => Outcome::Win,
        _ => panic!("no"),
    }
}
