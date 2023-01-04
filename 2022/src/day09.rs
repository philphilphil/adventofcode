use crate::base::{Problem, ProblemData};

pub struct Day9 {}

impl Problem for Day9 {
    fn part_one(&self, problem_data: &ProblemData) -> String {
        let motions: Vec<Motion> = get_motions(&problem_data.input);
        let mut visited = 1;

        for motion in motions {
            match motion {
                Motion::Right(stepps) => for i in 0..stepps {},
                Motion::Left(_) => todo!(),
                Motion::Down(_) => todo!(),
                Motion::Up(_) => todo!(),
            }
        }
        visited.to_string()
    }

    fn part_two(&self, problem_data: &ProblemData) -> String {
        "".to_owned()
    }
}

fn get_motions(input: &str) -> Vec<Motion> {
    let mut motions = vec![];

    for line in input.lines() {
        let mut split = line.split_whitespace();
        let direction = split.next().unwrap();
        let number = split.next().unwrap();
        let number = number.parse::<usize>().expect("cant parse number");

        match direction {
            "R" => motions.push(Motion::Right(number)),
            "L" => motions.push(Motion::Left(number)),
            "D" => motions.push(Motion::Down(number)),
            "U" => motions.push(Motion::Up(number)),
            _ => panic!("Invalid direction"),
        }
    }
    motions
}

#[derive(Debug)]
enum Motion {
    Right(usize),
    Left(usize),
    Down(usize),
    Up(usize),
}
