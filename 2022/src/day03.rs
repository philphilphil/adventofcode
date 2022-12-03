use std::io::BufRead;

use crate::base::{Problem, ProblemData};

pub struct Day3 {}

impl Problem for Day3 {
    fn part_one(&self, problem_data: &ProblemData) -> String {
        let mut score = 0;
        for line in problem_data.input.lines() {
            let compartment_split_at = line.len() / 2;
            let items_in_1_compartment = &line[..compartment_split_at];
            let items_in_2_compartment = line[compartment_split_at..].chars();

            for char in items_in_2_compartment {
                if items_in_1_compartment.contains(char) {
                    score += char_to_score(char);
                    break;
                }
            }
        }

        score.to_string()
    }

    fn part_two(&self, problem_data: &ProblemData) -> String {
        "".to_owned()
    }
}

fn char_to_score(char: char) -> i32 {
    if char.is_lowercase() {
        char as i32 - 96
    } else {
        char as i32 - 38
    }
}
