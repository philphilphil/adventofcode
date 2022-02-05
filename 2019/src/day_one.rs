use std::fs;

pub fn execute() {
    let filename = "Inputs/1";
    let data = fs::read_to_string(filename).expect("Unable to read file");

    part1(&data);
    part2(&data);
}

fn part1(data: &String) {
    let mut result = 0;

    for line in data.lines() {
        let mass = line.parse::<i32>().unwrap();
        let devided = (mass / 3) as f32;
        let rounded_down = devided.floor() as i32;
        let sub = rounded_down - 2;

        result += sub;
    }

    println!("Result Part 1: {}", result);
    assert_eq!(3305301, result);
}

fn part2(data: &String) {
    let mut result: i64 = 0;

    for line in data.lines() {
        let mut sub = line.parse::<i64>().unwrap();

        loop {
            sub = do_calculations(sub);

            if sub > 0 {
                result += sub;
            } else {
                break;
            }
        }
    }

    println!("Result Part 2: {}", result);
    assert_eq!(4955106, result);
}

fn do_calculations(mass: i64) -> i64 {
    let devided: f32 = (mass / 3) as f32;
    let rounded_down: i64 = devided.floor() as i64;
    rounded_down - 2
}
