use crate::base::{Problem, ProblemData};

pub struct Day10 {}

impl Problem for Day10 {
    fn part_one(&self, problem_data: &ProblemData) -> String {
        let instructions: Vec<Instruction> = get_instructions(&problem_data.input);
        let mut register_x = 1;
        let mut curr_cycle = 0;
        let mut sum_of_signals = 0;

        for instruction in instructions {
            curr_cycle += 1;
            // println!("Cycle:{} X:{} {:?}", curr_cycle, register_x, instruction);
            sum_up_cycles(&mut sum_of_signals, register_x, curr_cycle);

            match instruction {
                Instruction::Noop => continue,
                Instruction::AddX(number) => {
                    curr_cycle += 1;
                    register_x += number;
                    sum_up_cycles(&mut sum_of_signals, register_x, curr_cycle);
                }
            }
        }

        sum_of_signals.to_string()
    }

    fn part_two(&self, problem_data: &ProblemData) -> String {
        "".to_owned()
    }
}

fn sum_up_cycles(sum_of_signals: &mut i32, register_x: i32, curr_cycle: i32) {
    match curr_cycle {
        20 => *sum_of_signals += 20 * register_x,
        60 => *sum_of_signals += 60 * register_x,
        100 => *sum_of_signals += 100 * register_x,
        140 => *sum_of_signals += 140 * register_x,
        180 => *sum_of_signals += 180 * register_x,
        220 => *sum_of_signals += 220 * register_x,
        _ => {}
    }
}

fn get_instructions(input: &str) -> Vec<Instruction> {
    let mut instructions = vec![];

    for line in input.lines() {
        if line.starts_with("noop") {
            instructions.push(Instruction::Noop);
        } else if line.starts_with("addx") {
            let number = line.split_whitespace().nth(1).unwrap();
            let number = number.parse::<i32>().expect("cant parse number");
            instructions.push(Instruction::AddX(number));
        } else {
            panic!("Invalid Instruction found.");
        }
    }

    instructions
}

#[derive(Debug)]
enum Instruction {
    Noop,
    AddX(i32),
}
