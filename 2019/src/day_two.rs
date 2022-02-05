use std::fs;
use std::str::FromStr;

pub fn execute() {
    let filename = "Inputs/2";
    let data = fs::read_to_string(filename).expect("Unable to read file");
    let data_split_as_int: Vec<i32> = data.split(",").map(|s| i32::from_str(s).unwrap()).collect();
    part1(&mut data_split_as_int.clone());
    part2(&mut data_split_as_int.clone());
}

fn part1(data: &mut Vec<i32>) {
    data[1] = 12;
    data[2] = 2;

    let mut instruction_pointer = 0;

    loop {
        let pos_0 = data[instruction_pointer];
        let pos_1 = data[instruction_pointer + 1] as usize;
        let pos_2 = data[instruction_pointer + 2] as usize;
        let pos_3 = data[instruction_pointer + 3] as usize;

        match pos_0 {
            1 => data[pos_3] = data[pos_1] + data[pos_2],
            2 => data[pos_3] = data[pos_1] * data[pos_2],
            99 => break,
            _ => panic!("Something went wrong."),
        }

        instruction_pointer += 4;
    }

    println!("Result Part 1: {}", data[0]);
    assert_eq!(3516593, data[0]);
}

fn part2(data: &mut Vec<i32>) {
    let required_output = 19690720;

    let result = loop_calc(required_output, data);

    println!("Result Part 2: {}", result);
    assert_eq!(7749, result);
}

fn loop_calc(required_output: i32, data: &mut Vec<i32>) -> i32 {
    for noun in 0..99 {
        for verb in 0..99 {
            let calc_result = do_calculations(verb, noun, &mut data.clone());
            if calc_result == required_output {
                return 100 * verb + noun;
            }
        }
    }
    -1
}

fn do_calculations(noun: i32, verb: i32, data: &mut Vec<i32>) -> i32 {
    data[1] = noun;
    data[2] = verb;
    let mut instruction_pointer = 0;
    loop {
        let pos_0 = data[instruction_pointer];
        let pos_1 = data[instruction_pointer + 1] as usize;
        let pos_2 = data[instruction_pointer + 2] as usize;
        let pos_3 = data[instruction_pointer + 3] as usize;
        match pos_0 {
            1 => data[pos_3] = data[pos_1] + data[pos_2],
            2 => data[pos_3] = data[pos_1] * data[pos_2],
            99 => break,
            _ => panic!("Something went wrong."),
        }
        instruction_pointer += 4;
    }

    data[0]
}
