use crate::base::{Problem, ProblemData};

pub struct Day1 {}

impl Problem for Day1 {
    fn part_one(&self, problem_data: &ProblemData) -> String {
        let elves = sum_up_elves_calories(&problem_data.input);
        let elf_with_most_calories = elves.iter().max().unwrap();
        elf_with_most_calories.to_string()
    }

    fn part_two(&self, problem_data: &ProblemData) -> String {
        let mut elves = sum_up_elves_calories(&problem_data.input);
        elves.sort();
        elves.reverse();
        let top3_elves_with_most_calories: u32 = elves.iter().take(3).sum();
        top3_elves_with_most_calories.to_string()
    }
}

fn sum_up_elves_calories(input: &str) -> Vec<u32> {
    let mut elves = vec![];

    let mut curr_elve: u32 = 0;
    for line in input.lines() {
        if line.is_empty() {
            elves.push(curr_elve);
            curr_elve = 0;
            continue;
        }
        curr_elve += line.parse::<u32>().expect("cant parse number");
    }
    elves.push(curr_elve);

    elves
}
