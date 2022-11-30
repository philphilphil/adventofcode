mod base;
mod day01;
// mod day02;
// mod day03;
// mod day04;
// mod day05;
// mod day06;
// mod day07;
// mod day08;
// mod day09;
// mod day10;

use base::Problem;
use day01::Day1;
use std::env;

fn main() {
    let args: Vec<String> = env::args().skip(1).collect();

    let day: u8 = args[0].parse().expect("Can't parse day as u8.");

    let mut run_example = false;
    if args.len() > 1 {
        run_example = args[1].parse().expect("Can't parse example bool.");
    }

    let input: String = base::load_input(day, run_example);

    dbg!(&input);
    let day_to_execute = day_to_problem(day).expect("Issue getting problem.");
    day_to_execute.part_one(&input);
    day_to_execute.part_two(&input);
}

fn day_to_problem(day: u8) -> Option<Box<dyn Problem>> {
    match day {
        1 => Some(Box::new(Day1 {})),
        _ => None,
    }
}
