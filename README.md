# Esoteric Code Interpreter

An esoteric programming language (sometimes shortened to esolang) is a programming language designed as a test of the boundaries of computer programming language design, as a proof of concept, or as a joke. There is usually no intention of the language being adopted for real-world programming.

# Implemented languages

## Brainfuck

Brainfuck is the most popular esoteric language. There are in total 8 commands, each one character long.

#### Hello world in Brainfuck

	++++++++++[>+++++++>++++++++++>+++<<<-]>++.>+.+++++++..+++.>++.<<+++++++++++++++
	.>.+++.------.--------.>+.

### Ook!

Ook is a joke esoteric programming language. It is identical to Brainfuck, except that the instructions are changed into Orangutan words. The interpreter scans for `!`, `?` and `.` characters, then translates it into a Brainfuck code.

### Spoon

Spoon is also based on Brainfuck, but uses binary sequences to represent Brainfuck instructions converted through Huffman coding, with two additional commands added.

#### Hello world in Spoon

	01011111111100100011111111110100000011011001010010111111100100011111101000000110
	11100101011111110010100010101110010100101111111111100100011000000000000000000010
	00000110110000010100000000000000000000000000000000000000010100101111111111100100
	01111111010000001101100101001011111100100011111101000000110110010101110010100000
	00000000000000001010000000000000000000000000001010010111111111110010001100000000
	00000000000100000011011000001010

## Byter

Byter is a language for training brains. Byter consists of 11 instructions that are intended to move the instruction pointer on a 16×16 matrix and for outputting the characters which are assosiated with each cell. On an output operation the ascii character associated with the specific cell is printed out. The correct ascii value is determined by the position of the cell: It starts with zero in the top left corner of the matrix, and is increased from left to right.

### Playfield

	................
	................
	 !"#$%&'()*+,-./
	0123456789:;<=>?
	@ABCDEFGHIJKLMNO
	PQRSTUVWXYZ[\]^_
	`abcdefghijklmno
	pqrstuvwxyz{|}~.
	................
	................
	................
	................
	................
	................
	................
	................

### Hello world in Byter

	>>>>>>>>>V00V00V
	0#00A00V<V00V0VV
	-+>>A00VV<<0$0V>
	V0AA<00V>V<<<<V0
	>>V0V<<<-<000V<0
	0AV0VV<<<0000V<0
	0A>V+}>>>>>>}<V-
	0A{>>>>-000000>V
	00A0000>>>>>>>>A
	V0A<<<<<<<<<<<<>
	V000000000000000
	V000000000000000
	V000000000000000
	V000000000000000
	V000000000000000
	>>>>>>>>>>>>V000

## Whitespace

Whitespace is an esoteric programming language and it was released on 1 April 2003 (April Fool's Day). Its name is a reference to whitespace characters. Unlike most programming languages, which ignore or assign little meaning to most whitespace characters, the Whitespace interpreter ignores any non-whitespace characters. Only spaces, tabs and linefeeds have meaning. An interesting consequence of this property is that a Whitespace program can easily be contained within the whitespace characters of a program written in another language, making the text a polyglot.

The language itself is an imperative stack-based language. The virtual machine on which the programs run has a stack and a heap. The programmer is free to push arbitrary width integers onto the stack (currently there is no implementation of floating point numbers) and can also access the heap as a permanent store for variables and data structures.

### Hello world in Whitespace

`\s` is space, `\t` is tab and `\n` is line feed.

	\s\s\s\s\n\s\s\s\t\s\s\t\s\s\s\n\t\t\s\s\s\s\t\n\s\s\s\t\t\s\s\t\s\t\n\t\t\s\s\s
	\s\t\s\n\s\s\s\t\t\s\t\t\s\s\n\t\t\s\s\s\s\t\t\n\s\s\s\t\t\s\t\t\s\s\n\t\t\s\s\s
	\s\t\s\s\n\s\s\s\t\t\s\t\t\t\t\n\t\t\s\s\s\s\t\s\t\n\s\s\s\t\s\t\t\s\s\n\t\t\s\s
	\s\s\t\t\s\n\s\s\s\t\s\s\s\s\s\n\t\t\s\s\s\s\t\t\t\n\s\s\s\t\t\t\s\t\t\t\n\t\t\s
	\s\s\s\t\s\s\s\n\s\s\s\t\t\s\t\t\t\t\n\t\t\s\s\s\s\t\s\s\t\n\s\s\s\t\t\t\s\s\t\s
	\n\t\t\s\s\s\s\t\s\t\s\n\s\s\s\t\t\s\t\t\s\s\n\t\t\s\s\s\s\t\s\t\t\n\s\s\s\t\t\s
	\s\t\s\s\n\t\t\s\s\s\s\t\t\s\s\n\s\s\s\t\s\s\s\s\s\n\t\t\s\s\s\s\t\t\s\t\n\s\s\s
	\t\t\s\t\t\t\t\n\t\t\s\s\s\s\t\t\t\s\n\s\s\s\t\t\s\s\t\t\s\n\t\t\s\s\s\s\t\t\t\t
	\n\s\s\s\t\s\s\s\s\s\n\t\t\s\s\s\s\t\s\s\s\s\n\s\s\s\t\t\t\s\s\t\t\n\t\t\s\s\s\s
	\t\s\s\s\t\n\s\s\s\t\t\t\s\s\s\s\n\t\t\s\s\s\s\t\s\s\t\s\n\s\s\s\t\t\s\s\s\s\t\n
	\t\t\s\s\s\s\t\s\s\t\t\n\s\s\s\t\t\s\s\s\t\t\n\t\t\s\s\s\s\t\s\t\s\s\n\s\s\s\t\t
	\s\s\t\s\t\n\t\t\s\s\s\s\t\s\t\s\t\n\s\s\s\t\t\t\s\s\t\t\n\t\t\s\s\s\s\t\s\t\t\s
	\n\s\s\s\t\s\s\s\s\t\n\t\t\s\s\s\s\t\s\t\t\t\n\s\s\s\s\n\t\t\s\s\s\s\s\n\n\s\t\s
	\t\t\t\s\t\t\t\s\t\t\t\s\s\t\s\s\t\t\s\t\s\s\t\s\t\t\t\s\t\s\s\s\t\t\s\s\t\s\t\n
	\n\s\t\s\t\t\s\t\t\t\s\s\t\t\s\s\t\s\t\s\t\t\t\s\t\t\t\s\t\t\s\t\t\s\s\s\t\t\s\t
	\s\s\t\s\t\t\s\t\t\t\s\s\t\t\s\s\t\s\t\n\n\n\n\n\s\s\s\t\t\s\s\s\s\t\s\t\t\s\s\t
	\s\s\s\t\t\s\s\t\s\s\n\t\s\s\s\n\t\n\n\s\s\s\t\t\t\s\t\t\t\s\t\t\t\s\s\t\s\s\t\t
	\s\t\s\s\t\s\t\t\t\s\t\s\s\s\t\t\s\s\t\s\t\n\s\n\s\t\t\t\s\n\s\n\t\s\s\t\t\t\s\t
	\t\t\s\t\t\t\s\s\t\s\s\t\t\s\t\s\s\t\s\t\t\t\s\t\s\s\s\t\t\s\s\t\s\t\s\t\s\t\t\t
	\t\t\s\t\t\s\s\t\s\t\s\t\t\s\t\t\t\s\s\t\t\s\s\t\s\s\n\t\n\s\s\s\s\s\t\n\t\s\s\s
	\n\s\n\s\t\t\t\s\t\t\t\s\t\t\t\s\s\t\s\s\t\t\s\t\s\s\t\s\t\t\t\s\t\s\s\s\t\t\s\s
	\t\s\t\n\n\s\s\s\t\t\t\s\t\t\t\s\t\t\t\s\s\t\s\s\t\t\s\t\s\s\t\s\t\t\t\s\t\s\s\s
	\t\t\s\s\t\s\t\s\t\s\t\t\t\t\t\s\t\t\s\s\t\s\t\s\t\t\s\t\t\t\s\s\t\t\s\s\t\s\s\n
	\s\n\n\s\n\n\n\t\n\n\s\s\s\t\t\t\s\s\t\s\s\t\t\s\s\t\s\t\s\t\t\s\s\s\s\t\s\t\t\s
	\s\t\s\s\n\s\n\s\s\n\s\t\n\t\s\t\t\t\s\n\s\s\s\s\t\s\t\s\n\t\s\s\t\n\t\s\s\t\t\t
	\s\s\t\s\s\t\t\s\s\t\s\t\s\t\t\s\s\s\s\t\s\t\t\s\s\t\s\s\s\t\s\t\t\t\t\t\s\t\t\s
	\s\t\s\t\s\t\t\s\t\t\t\s\s\t\t\s\s\t\s\s\n\s\n\n\s\s\s\t\n\t\s\s\s\n\s\n\s\t\t\t
	\s\s\t\s\s\t\t\s\s\t\s\t\s\t\t\s\s\s\s\t\s\t\t\s\s\t\s\s\n\n\s\s\s\t\t\t\s\s\t\s
	\s\t\t\s\s\t\s\t\s\t\t\s\s\s\s\t\s\t\t\s\s\t\s\s\s\t\s\t\t\t\t\t\s\t\t\s\s\t\s\t
	\s\t\t\s\t\t\t\s\s\t\t\s\s\t\s\s\n\s\n\n\s\s\s\t\n\t\s\s\s\s\s\s\s\n\t\t\s\n\t\n
	\n\s\s\s\t\t\s\t\t\t\s\s\t\t\s\s\t\s\t\s\t\t\t\s\t\t\t\s\t\t\s\t\t\s\s\s\t\t\s\t
	\s\s\t\s\t\t\s\t\t\t\s\s\t\t\s\s\t\s\t\n\s\s\s\t\s\t\s\n\s\s\s\t\t\s\t\n\t\n\s\s
	\t\n\s\s\n\t\n

The interpreter can translate the Whitespace code into an assembly-like code. The above Hello world code translated looks like this:

	[0013] push 0
	[0024] push 72 ; ascii: H
	[0027] store
	[0032] push 1
	[0043] push 101 ; ascii: e
	[0046] store
	[0052] push 2
	[0063] push 108 ; ascii: l
	[0066] store
	[0072] push 3
	[0083] push 108 ; ascii: l
	[0086] store
	[0093] push 4
	[0104] push 111 ; ascii: o
	[0107] store
	[0114] push 5
	[0124] push 44 ; ascii: ,
	[0127] store
	[0134] push 6
	[0144] push 32 ; ascii:
	[0147] store
	[0154] push 7
	[0165] push 119 ; ascii: w
	[0168] store
	[0176] push 8
	[0187] push 111 ; ascii: o
	[0190] store
	[0198] push 9
	[0209] push 114 ; ascii: r
	[0212] store
	[0220] push 10
	[0231] push 108 ; ascii: l
	[0234] store
	[0242] push 11
	[0253] push 100 ; ascii: d
	[0256] store
	[0264] push 12
	[0274] push 32 ; ascii:
	[0277] store
	[0285] push 13
	[0296] push 111 ; ascii: o
	[0299] store
	[0307] push 14
	[0318] push 102 ; ascii: f
	[0321] store
	[0329] push 15
	[0339] push 32 ; ascii:
	[0342] store
	[0351] push 16
	[0362] push 115 ; ascii: s
	[0365] store
	[0374] push 17
	[0385] push 112 ; ascii: p
	[0388] store
	[0397] push 18
	[0408] push 97 ; ascii: a
	[0411] store
	[0420] push 19
	[0431] push 99 ; ascii: c
	[0434] store
	[0443] push 20
	[0454] push 101 ; ascii: e
	[0457] store
	[0466] push 21
	[0477] push 115 ; ascii: s
	[0480] store
	[0489] push 22
	[0499] push 33 ; ascii: !
	[0502] store
	[0511] push 23
	[0516] push 0
	[0519] store
	[0524] push 0
	[0568] call 0111011101110010011010010111010001100101
	[0713] dup
	[0716] retr
	[0719] dup
	[0795] jz 011101110111001001101001011101000110010101011111011001010110
	[0799] outchar
	[0799]> H
	[0804] push 1
	[0808] add
	[0852] jmp 0111011101110010011010010111010001100101
	[0713] dup
	[0716] retr
	[0719] dup
	[0795] jz 011101110111001001101001011101000110010101011111011001010110
	[0799] outchar
	[0799]> e
	[0804] push 1
	[0808] add
	[0852] jmp 0111011101110010011010010111010001100101
	[0713] dup
	[0716] retr
	[0719] dup
	[0795] jz 011101110111001001101001011101000110010101011111011001010110
	[0799] outchar
	[0799]> l
	[0804] push 1
	[0808] add
	[0852] jmp 0111011101110010011010010111010001100101
	[0713] dup
	[0716] retr
	[0719] dup
	[0795] jz 011101110111001001101001011101000110010101011111011001010110
	[0799] outchar
	[0799]> l
	[0804] push 1
	[0808] add
	[0852] jmp 0111011101110010011010010111010001100101
	[0713] dup
	[0716] retr
	[0719] dup
	[0795] jz 011101110111001001101001011101000110010101011111011001010110
	[0799] outchar
	[0799]> o
	[0804] push 1
	[0808] add
	[0852] jmp 0111011101110010011010010111010001100101
	[0713] dup
	[0716] retr
	[0719] dup
	[0795] jz 011101110111001001101001011101000110010101011111011001010110
	[0799] outchar
	[0799]> ,
	[0804] push 1
	[0808] add
	[0852] jmp 0111011101110010011010010111010001100101
	[0713] dup
	[0716] retr
	[0719] dup
	[0795] jz 011101110111001001101001011101000110010101011111011001010110
	[0799] outchar
	[0799]>
	[0804] push 1
	[0808] add
	[0852] jmp 0111011101110010011010010111010001100101
	[0713] dup
	[0716] retr
	[0719] dup
	[0795] jz 011101110111001001101001011101000110010101011111011001010110
	[0799] outchar
	[0799]> w
	[0804] push 1
	[0808] add
	[0852] jmp 0111011101110010011010010111010001100101
	[0713] dup
	[0716] retr
	[0719] dup
	[0795] jz 011101110111001001101001011101000110010101011111011001010110
	[0799] outchar
	[0799]> o
	[0804] push 1
	[0808] add
	[0852] jmp 0111011101110010011010010111010001100101
	[0713] dup
	[0716] retr
	[0719] dup
	[0795] jz 011101110111001001101001011101000110010101011111011001010110
	[0799] outchar
	[0799]> r
	[0804] push 1
	[0808] add
	[0852] jmp 0111011101110010011010010111010001100101
	[0713] dup
	[0716] retr
	[0719] dup
	[0795] jz 011101110111001001101001011101000110010101011111011001010110
	[0799] outchar
	[0799]> l
	[0804] push 1
	[0808] add
	[0852] jmp 0111011101110010011010010111010001100101
	[0713] dup
	[0716] retr
	[0719] dup
	[0795] jz 011101110111001001101001011101000110010101011111011001010110
	[0799] outchar
	[0799]> d
	[0804] push 1
	[0808] add
	[0852] jmp 0111011101110010011010010111010001100101
	[0713] dup
	[0716] retr
	[0719] dup
	[0795] jz 011101110111001001101001011101000110010101011111011001010110
	[0799] outchar
	[0799]>
	[0804] push 1
	[0808] add
	[0852] jmp 0111011101110010011010010111010001100101
	[0713] dup
	[0716] retr
	[0719] dup
	[0795] jz 011101110111001001101001011101000110010101011111011001010110
	[0799] outchar
	[0799]> o
	[0804] push 1
	[0808] add
	[0852] jmp 0111011101110010011010010111010001100101
	[0713] dup
	[0716] retr
	[0719] dup
	[0795] jz 011101110111001001101001011101000110010101011111011001010110
	[0799] outchar
	[0799]> f
	[0804] push 1
	[0808] add
	[0852] jmp 0111011101110010011010010111010001100101
	[0713] dup
	[0716] retr
	[0719] dup
	[0795] jz 011101110111001001101001011101000110010101011111011001010110
	[0799] outchar
	[0799]>
	[0804] push 1
	[0808] add
	[0852] jmp 0111011101110010011010010111010001100101
	[0713] dup
	[0716] retr
	[0719] dup
	[0795] jz 011101110111001001101001011101000110010101011111011001010110
	[0799] outchar
	[0799]> s
	[0804] push 1
	[0808] add
	[0852] jmp 0111011101110010011010010111010001100101
	[0713] dup
	[0716] retr
	[0719] dup
	[0795] jz 011101110111001001101001011101000110010101011111011001010110
	[0799] outchar
	[0799]> p
	[0804] push 1
	[0808] add
	[0852] jmp 0111011101110010011010010111010001100101
	[0713] dup
	[0716] retr
	[0719] dup
	[0795] jz 011101110111001001101001011101000110010101011111011001010110
	[0799] outchar
	[0799]> a
	[0804] push 1
	[0808] add
	[0852] jmp 0111011101110010011010010111010001100101
	[0713] dup
	[0716] retr
	[0719] dup
	[0795] jz 011101110111001001101001011101000110010101011111011001010110
	[0799] outchar
	[0799]> c
	[0804] push 1
	[0808] add
	[0852] jmp 0111011101110010011010010111010001100101
	[0713] dup
	[0716] retr
	[0719] dup
	[0795] jz 011101110111001001101001011101000110010101011111011001010110
	[0799] outchar
	[0799]> e
	[0804] push 1
	[0808] add
	[0852] jmp 0111011101110010011010010111010001100101
	[0713] dup
	[0716] retr
	[0719] dup
	[0795] jz 011101110111001001101001011101000110010101011111011001010110
	[0799] outchar
	[0799]> s
	[0804] push 1
	[0808] add
	[0852] jmp 0111011101110010011010010111010001100101
	[0713] dup
	[0716] retr
	[0719] dup
	[0795] jz 011101110111001001101001011101000110010101011111011001010110
	[0799] outchar
	[0799]> !
	[0804] push 1
	[0808] add
	[0852] jmp 0111011101110010011010010111010001100101
	[0713] dup
	[0716] retr
	[0719] dup
	[0795] jz 011101110111001001101001011101000110010101011111011001010110
	[0931] pop
	[0934] pop
	[0937] ret
	[0628] call 01101110011001010111011101101100011010010110111001100101
	[1276] push 10
	[1284] push 13
	[1288] outchar
	[1288]>
	[1292] outchar
	[1292]> \n
	[1295] ret
	[0631] exit