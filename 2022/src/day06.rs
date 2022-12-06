use crate::base::{Problem, ProblemData};

pub struct Day6 {}

impl Problem for Day6 {
    fn part_one(&self, problem_data: &ProblemData) -> String {
        let marker_position = find_start_of_packet_marker(&problem_data.input, 4);
        marker_position.to_string()
    }

    fn part_two(&self, problem_data: &ProblemData) -> String {
        let marker_position = find_start_of_packet_marker(&problem_data.input, 14);
        marker_position.to_string()
    }
}

fn find_start_of_packet_marker(datastream: &str, seq_len: usize) -> usize {
    let mut sequence_chars = vec![];

    for (num, char) in datastream.chars().enumerate() {
        if sequence_chars.len() < seq_len {
            sequence_chars.push(char);
            continue;
        } else {
            sequence_chars.remove(0);
            sequence_chars.push(char);
        }

        // NOTE: this should be nicer with a HashSet
        let mut deduped = sequence_chars.clone();
        deduped.sort();
        deduped.dedup();
        if deduped.len() == seq_len {
            return num + 1;
        }
    }
    0
}
