mod base;
mod day01;
mod day02;
mod day03;
mod day04;
mod day05;
mod day06;
mod day07;
mod day08;
mod day09;
mod day10;

use base::Problem;
use day01::Day1;
use day02::Day2;
use day03::Day3;
use day04::Day4;
use day05::Day5;
use day06::Day6;
use day07::Day7;
use day08::Day8;
use day09::Day9;
use day10::Day10;
use std::env;

fn main() {
    let args: Vec<String> = env::args().skip(1).collect();

    let day: u8 = args[0].parse().expect("Can't parse day as u8.");

    let mut run_example = false;
    if args.len() > 1 {
        run_example = args[1].parse().expect("Can't parse example bool.");
    }

    let problem_data = base::ProblemData::new(day, run_example);
    // dbg!(&problem_data);

    let day_to_execute = day_to_problem(day).expect("Issue getting problem.");
    let result_part1 = day_to_execute.part_one(&problem_data);
    let result_part2 = day_to_execute.part_two(&problem_data);

    assert_eq!(problem_data.p1_result, result_part1);
    assert_eq!(problem_data.p2_result, result_part2);

    println!(
        "Day {}. Part 1: {}, Part 2: {}",
        args[0], result_part1, result_part2
    );
}

fn day_to_problem(day: u8) -> Option<Box<dyn Problem>> {
    match day {
        1 => Some(Box::new(Day1 {})),
        2 => Some(Box::new(Day2 {})),
        3 => Some(Box::new(Day3 {})),
        4 => Some(Box::new(Day4 {})),
        5 => Some(Box::new(Day5 {})),
        6 => Some(Box::new(Day6 {})),
        7 => Some(Box::new(Day7 {})),
        8 => Some(Box::new(Day8 {})),
        9 => Some(Box::new(Day9 {})),
        10 => Some(Box::new(Day10 {})),
        _ => None,
    }
}
