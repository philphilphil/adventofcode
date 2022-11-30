use std::fs;

pub trait Problem {
    fn part_one(&self, input: &str) -> String;
    fn part_two(&self, input: &str) -> String;
}

pub fn load_input(day_numbner: u8, example: bool) -> String {
    let filename = get_day_path(day_numbner, example);
    fs::read_to_string(filename).expect("Unable to read file")
}

/// Gets the filepath to the current day
/// Optional gets the path to the little example
fn get_day_path(day_number: u8, example: bool) -> String {
    let mut path: String = "Inputs/".to_owned();
    if day_number <= 9 {
        path.push('0');
    }
    path.push_str(&day_number.to_string());

    if example {
        path.push_str("_example");
    }
    dbg!(&path);

    path
}
