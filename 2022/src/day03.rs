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
        let mut score = 0;
        let mut line_iter = problem_data.input.lines().peekable();
        'outer: while let Some(line) = line_iter.next() {
            let groups_first_content = line.to_owned();
            let groups_second_content = line_iter.next().unwrap();
            let groups_third_content = line_iter.next().unwrap();

            for char1 in groups_first_content.chars() {
                if groups_second_content.contains(char1) {
                    for _ in groups_second_content.chars() {
                        if groups_third_content.contains(char1) {
                            score += char_to_score(char1);
                            continue 'outer;
                        }
                    }
                }
            }
        }

        score.to_string()
    }
}

fn char_to_score(char: char) -> i32 {
    if char.is_lowercase() {
        char as i32 - 96
    } else {
        char as i32 - 38
    }
}
