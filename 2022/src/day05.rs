use crate::base::{Problem, ProblemData};

pub struct Day5 {}

impl Problem for Day5 {
    fn part_one(&self, problem_data: &ProblemData) -> String {
        let mut input_split = problem_data.input.split("\n\n");
        let starting_stack = input_split.next().unwrap();
        let instructions = input_split.next().unwrap();

        let mut crane = parse_starting_stacks(starting_stack);

        for instruction in instructions.lines() {
            let (from, to, count) = parse_instruction(instruction);

            (0..count).for_each(|_| {
                let crate_ = crane[from - 1].pop().unwrap();
                crane[to - 1].push(crate_);
            });
        }

        crane.iter().map(|s| s.last().unwrap()).collect::<String>()
    }

    fn part_two(&self, problem_data: &ProblemData) -> String {
        let mut input_split = problem_data.input.split("\n\n");
        let starting_stack = input_split.next().unwrap();
        let instructions = input_split.next().unwrap();

        let mut crane = parse_starting_stacks(starting_stack);

        for instruction in instructions.lines() {
            let (from, to, count) = parse_instruction(instruction);

            let keep_crates_till = crane[from - 1].len() - count;

            // first push all like before, working up from the first crate to move
            (0..count).for_each(|i| {
                let crate_ = crane[from - 1][keep_crates_till + i];
                crane[to - 1].push(crate_);
            });

            // but now cut them from original vec
            crane[from - 1].truncate(keep_crates_till);
        }

        crane.iter().map(|s| s.last().unwrap()).collect::<String>()
    }
}

fn parse_starting_stacks(input: &str) -> Vec<Vec<char>> {
    let mut crane: Vec<Vec<char>> = vec![];

    for line in input.lines().rev().skip(1) {
        for (num, char) in line.chars().skip(1).step_by(4).enumerate() {
            if char.is_alphabetic() {
                if crane.len() <= num {
                    crane.push(vec![]);
                }
                crane[num].push(char);
            }
        }
    }
    crane
}

fn parse_instruction(instruction: &str) -> (usize, usize, usize) {
    let mut split = instruction.split_whitespace();
    split.next();
    let count = split.next().unwrap().parse::<usize>().unwrap();
    split.next();
    let from = split.next().unwrap().parse::<usize>().unwrap();
    split.next();
    let to = split.next().unwrap().parse::<usize>().unwrap();

    (from, to, count)
}
